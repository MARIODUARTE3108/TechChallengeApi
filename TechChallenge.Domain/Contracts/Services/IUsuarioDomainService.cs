using Microsoft.AspNetCore.Identity;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Contracts.Services
{
    public interface IUsuarioDomainService : IDisposable
    {
        Task<Usuario> Inserir(Usuario usuario);
        Task<Usuario> VerificarLogin(string email, string senha);
        Task<Usuario> BuscarPorId(int id);
        Task<Usuario> BuscarPorEmail(string email);
        Task<ICollection<Usuario>> ListarTudo();
    }
}
