using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Contracts.Services
{
    public interface INoticiaDomainService : IDisposable
    {
        Task<Noticia> Inserir(Noticia noticia);
        Task<Noticia> BuscarPorId(int id);
        Task<ICollection<Noticia>> ListarTudo();
    }
}
