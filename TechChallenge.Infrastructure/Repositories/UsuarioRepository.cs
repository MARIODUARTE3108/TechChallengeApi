using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SisAgenda.Infra.Seguranca.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Domain.Contracts.Datas;
using TechChallenge.Domain.Entities;
using TechChallenge.Infrastructure.Context;
using TechChallenge.Infrastructure.Migrations;
using TechChallenge.Infrastructure.Security;

namespace TechChallenge.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SqlServerContext _contexto;

        public UsuarioRepository()
        {
            _contexto = new SqlServerContext();
        }
        public async Task<Usuario> VerificarLogin(string email, string senha)
        {
            senha = CriptografiaServico.Encrypt(senha);
            return _contexto.Usuario.FirstOrDefault(c => c.Email == email && c.Senha == senha);
        }

        public async Task<Usuario> BuscarPorEmail(string email)
        {
            return _contexto.Usuario.Where(x => x.Email == email).FirstOrDefault();
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }

        public Usuario Get(string email)
        {
            return _contexto.Usuario.Where(x => x.Email == email).FirstOrDefault();
        }

        public async Task<ICollection<Usuario>> ListarTudo()
        {
            return  _contexto.Usuario.ToList();
        }

        public async Task<Usuario> BuscarPorId(int id)
        {
            return  _contexto.Usuario.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<Usuario> Inserir(Usuario usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            _contexto.Usuario.Add(usuario);
            _contexto.SaveChanges();

            return usuario;
        }
    }
}
