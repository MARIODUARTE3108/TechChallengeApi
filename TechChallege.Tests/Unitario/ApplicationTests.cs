using Bogus;
using FluentAssertions.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SisAgenda.Infra.Seguranca.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Api.Settings;
using TechChallenge.Application.Contracts;
using TechChallenge.Application.Models;
using TechChallenge.Application.Services;
using TechChallenge.Domain.Contracts.Datas;
using TechChallenge.Domain.Contracts.Services;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Service;
using TechChallenge.Infrastructure.Repositories;

namespace TechChallege.Tests.Unitario
{
    public class ApplicationTests
    {
        private readonly IUsuarioApplicationService _usuarioApplication;
        private readonly INoticiaApplicationService _noticiaApplication;

        public ApplicationTests()
        {
            var servico = new ServiceCollection();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            servico.AddSingleton<IConfiguration>(config);
            AppSettings.ConnectionStrings = config.GetConnectionString("UsuarioConnection");

            servico.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
            servico.AddTransient<INoticiaDomainService, NoticiaDomainService>();
            servico.AddTransient<IUsuarioRepository, UsuarioRepository>();
            servico.AddTransient<INoticiaRepository, NoticiaRepository>();
            servico.AddTransient<IUsuarioApplicationService, UsuarioApplicationService>();
            servico.AddTransient<INoticiaApplicationService, NoticiaApplicationService>();
            servico.AddScoped<TokenApplicationService>();
            servico.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var provedor = servico.BuildServiceProvider();

            _usuarioApplication = provedor.GetService<IUsuarioApplicationService>();
            _noticiaApplication = provedor.GetService<INoticiaApplicationService>();
        }

        [Fact]
        public async Task<Usuario> Cadastrar_Usuario()
        {
            var faker = new Faker("pt_BR");

            var senha = "Du@rte3108+";

            var model = new UsuarioModel
            {
                Nome = faker.Person.FullName,
                Email = faker.Person.Email,
                Senha = senha,
                SenhaConfirmacao = senha,
            };

            var usuario = await _usuarioApplication.Inserir(model);
            Assert.NotNull(usuario);
            usuario.Senha = senha;
            return usuario;
        }
        [Fact]
        public async Task Buscar_Por_Email()
        {
            var usuario = await new RepositoryTests().Cadastrar_Usuario();
            var login = await _usuarioApplication.BuscarPorEmail(usuario.Email);

            Assert.NotNull(login);
        }
        [Fact]
        public async Task Todos_Usuarios()
        {
            ICollection<Usuario> lista = await _usuarioApplication.ListarTudo();

            Assert.NotNull(lista);
        }
        [Fact]
        public async Task Usuario_Por_Id()
        {
            var usuario = await new RepositoryTests().Cadastrar_Usuario();

            Usuario usuarioPorId = await _usuarioApplication.BuscarPorId(usuario.Id);

            Assert.NotNull(usuarioPorId);
        }
        [Fact]
        public async Task Get_Access_Token()
        {
            var usuario = await new RepositoryTests().Cadastrar_Usuario();
            
            var model = new LoginModel
            {
                Email = usuario.Email,
                Senha = usuario.Senha,
            };

            var token = _usuarioApplication.GetAccessToken(model);

            Assert.NotNull(token);
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

            var noticia = await _noticiaApplication.Inserir(model);
            Assert.NotNull(noticia);
            return noticia;
        }
        [Fact]
        public async Task Todas_Noticias()
        {
            ICollection<Noticia> lista = await _noticiaApplication.ListarTudo();

            Assert.NotNull(lista);
        }
        [Fact]
        public async Task Noticia_Por_Id()
        {
            var noticia = new RepositoryTests().Cadastrar_Noticia();

            Noticia usuarioPorId = await _noticiaApplication.BuscarPorId(noticia.Id);

            Assert.NotNull(usuarioPorId);
        }
    }
}
