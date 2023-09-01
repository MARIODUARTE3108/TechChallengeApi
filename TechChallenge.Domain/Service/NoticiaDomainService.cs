using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Domain.Contracts.Datas;
using TechChallenge.Domain.Contracts.Services;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Service
{
    public class NoticiaDomainService : INoticiaDomainService
    {
        private readonly INoticiaRepository _repository;

        public NoticiaDomainService(INoticiaRepository repository)
        {
            _repository = repository;
        }

        public Task<Noticia> Inserir(Noticia noticia)
        {
            return _repository.Inserir(noticia);
        }

        public Task<Noticia> BuscarPorId(int id)
        {
            return _repository.BuscarPorId(id);
        }

        public Task<ICollection<Noticia>> ListarTudo()
        {
            return _repository.ListarTudo();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
