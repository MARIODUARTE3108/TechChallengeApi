using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TechChallege.Tests.Helper;
using TechChallege.Tests.Integracao;
using TechChallenge.Application.Models;
using TechChallenge.Domain.Contracts.Datas;
using TechChallenge.Domain.Entities;
using TechChallenge.Infrastructure.Repositories;
using System.Collections;
using SisAgenda.Infra.Seguranca.Servico;
using Microsoft.Extensions.Configuration;
using TechChallenge.Api.Settings;

namespace TechChallege.Tests.Unitario
{
    public class RepositoryTests
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly INoticiaRepository _noticiaRepository;
        public RepositoryTests()
        {
            var servico = new ServiceCollection();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            servico.AddSingleton<IConfiguration>(config);
            AppSettings.ConnectionStrings = config.GetConnectionString("UsuarioConnection");

            servico.AddTransient<IUsuarioRepository, UsuarioRepository>();
            servico.AddTransient<INoticiaRepository, NoticiaRepository>();
            var provedor = servico.BuildServiceProvider();
            _usuarioRepository = provedor.GetService<IUsuarioRepository>();
            _noticiaRepository = provedor.GetService<INoticiaRepository>();
        }
        [Fact]
        public async Task<Usuario> Cadastrar_Usuario()
        {
            var faker = new Faker("pt_BR");

            var senha = "Du@rte3108+";

            var model = new Usuario
            {
                Nome = faker.Person.FullName,
                Email = faker.Person.Email,
                Senha = CriptografiaServico.Encrypt(senha),
            };
           
            var usuario =  await _usuarioRepository.Inserir(model);
            Assert.NotNull(usuario);
            usuario.Senha= senha;
            return usuario;
        }
        [Fact]
        public async Task Verifica_Login()
        {
            var usuario = await new RepositoryTests().Cadastrar_Usuario();
            var login = await _usuarioRepository.VerificarLogin(usuario.Email, usuario.Senha);

            Assert.NotNull(login);
        }
        [Fact]
        public async Task Buscar_Por_Email()
        {
            var usuario = await new RepositoryTests().Cadastrar_Usuario();
            var login = await _usuarioRepository.BuscarPorEmail(usuario.Email);

            Assert.NotNull(login);
        }
        [Fact]
        public async Task Todos_Usuarios()
        {
            ICollection<Usuario> lista = await _usuarioRepository.ListarTudo();

            Assert.NotNull(lista);
        }
        [Fact]
        public async Task Usuario_Por_Id()
        {
            var usuario = await new RepositoryTests().Cadastrar_Usuario();

            Usuario usuarioPorId = await _usuarioRepository.BuscarPorId(usuario.Id);

            Assert.NotNull(usuarioPorId);
        }

        [Fact]
        public async Task<Noticia> Cadastrar_Noticia()
        {
            var faker = new Faker("pt_BR");
            var model = new Noticia()
            {
                Titulo = faker.Lorem.Sentence(),
                Descricao = faker.Lorem.Paragraphs(3),
                Chapeu = faker.Lorem.Sentence(),
                DataPublicacao = DateTime.Now,
                Autor = faker.Person.FirstName
            };
   
            var noticia = await _noticiaRepository.Inserir(model);
            Assert.NotNull(noticia);
            return noticia;
        }
        [Fact]
        public async Task Todas_Noticias()
        {
            ICollection<Noticia> lista = await _noticiaRepository.ListarTudo();

            Assert.NotNull(lista);
        }
        [Fact]
        public async Task Noticia_Por_Id()
        {
            var noticia =  new RepositoryTests().Cadastrar_Noticia();

            Noticia usuarioPorId = await _noticiaRepository.BuscarPorId(noticia.Id);

            Assert.NotNull(usuarioPorId);
        }
    }
}
