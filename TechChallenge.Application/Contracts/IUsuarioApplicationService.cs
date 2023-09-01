using Microsoft.AspNetCore.Identity;
using TechChallenge.Application.Models;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Application.Contracts
{
    public interface IUsuarioApplicationService : IDisposable
    {
        Task<Usuario> Inserir(UsuarioModel usuario);
        Task<Usuario> BuscarPorId(int id);
        Task<Usuario> BuscarPorEmail(string email);
        Task<ICollection<Usuario>> ListarTudo();
        public string GetAccessToken(LoginModel usuario);
    }
}
