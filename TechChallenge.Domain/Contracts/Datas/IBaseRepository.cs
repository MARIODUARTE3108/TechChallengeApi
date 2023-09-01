using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Contracts.Datas
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<ICollection<TEntity>> ListarTudo();
        Task<TEntity> BuscarPorId(int id);
        Task<TEntity> Inserir(TEntity usuario);
    }
}
