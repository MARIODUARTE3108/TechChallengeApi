using Microsoft.AspNetCore.Identity;
using TechChallenge.Domain.Contracts.Datas;
using TechChallenge.Domain.Contracts.Services;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Service
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioDomainService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public Task<Usuario> Inserir(Usuario usuario)
        {
            return _repository.Inserir(usuario);
        }

        public Task<Usuario> VerificarLogin(string email, string senha)
        {
            return _repository.VerificarLogin(email, senha);
        }

        public Task<Usuario> BuscarPorId(int id)
        {
            return _repository.BuscarPorId(id);
        }

        public Task<Usuario> BuscarPorEmail(string email)
        {
            return _repository.BuscarPorEmail(email);
        }

        public Task<ICollection<Usuario>> ListarTudo()
        {
            return _repository.ListarTudo();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
