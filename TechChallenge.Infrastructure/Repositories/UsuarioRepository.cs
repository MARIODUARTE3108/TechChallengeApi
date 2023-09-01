using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SisAgenda.Infra.Seguranca.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Domain.Contracts.Datas;
using TechChallenge.Domain.Entities;
using TechChallenge.Infrastructure.Context;
using TechChallenge.Infrastructure.Security;

namespace TechChallenge.Infrastructure.Repositories
{
    public class UsuarioRepository : BaseReposiitory<Usuario>, IUsuarioRepository
    {
        protected readonly SqlServerContext contexto;
   

        public UsuarioRepository(SqlServerContext sqlServerContext) : base(sqlServerContext)
        {
            contexto = sqlServerContext;
        }
        public async Task<Usuario> VerificarLogin(string email, string senha)
        {
            senha = CriptografiaServico.Encrypt(senha);
            return contexto.Usuario.FirstOrDefault(c => c.Email == email && c.Senha == senha);
        }

        public async Task<Usuario> BuscarPorEmail(string email)
        {
            return contexto.Usuario.Where(x => x.Email == email).FirstOrDefault();
        }

        public void Dispose()
        {
            _ctx.Dispose();
            GC.SuppressFinalize(this);
        }

        public Usuario Get(string email)
        {
            return contexto.Usuario.Where(x=>x.Email== email).FirstOrDefault();
        }
    }
}
