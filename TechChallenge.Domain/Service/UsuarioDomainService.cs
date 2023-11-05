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

        public async Task<Usuario> VerificarLogin(string email, string senha)
        {
            return await _repository.VerificarLogin(email, senha);
        }

        public async Task<Usuario> BuscarPorId(int id)
        {
            return await _repository.BuscarPorId(id);
        }

        public async Task<Usuario> BuscarPorEmail(string email)
        {
            return await _repository.BuscarPorEmail(email);
        }

        public  async Task<ICollection<Usuario>> ListarTudo()
        {
            return await _repository.ListarTudo();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
