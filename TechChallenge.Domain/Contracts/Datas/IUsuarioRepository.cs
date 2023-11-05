using Microsoft.AspNetCore.Identity;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Contracts.Datas
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario> VerificarLogin(string email, string senha);
        Task<Usuario> BuscarPorEmail(string email);
    }
}
