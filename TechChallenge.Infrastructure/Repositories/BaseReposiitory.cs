using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Domain.Contracts.Datas;
using TechChallenge.Domain.Entities;
using TechChallenge.Infrastructure.Context;

namespace TechChallenge.Infrastructure.Repositories
{
    //public class BaseReposiitory<TEntity> : IBaseRepository<TEntity> where TEntity : class
    //{
    //    protected readonly SqlServerContext _ctx;
    //    protected readonly DbSet<TEntity> _dbSet;

    //    public BaseReposiitory(SqlServerContext ctx)
    //    {
    //        _ctx = ctx;
    //        _dbSet = ctx.Set<TEntity>();
    //    }
    //    public async Task<ICollection<TEntity>> ListarTudo()
    //    {
    //        return _dbSet.ToList();
    //    }

    //    public async Task<TEntity> BuscarPorId(int id)
    //    {
    //        return _dbSet.Find(id);
    //    }

    //    public async Task<TEntity> Inserir(TEntity objeto)
    //    {
    //        using var trans = _ctx.Database.BeginTransaction();
    //        try
    //        {
    //            _ctx.Entry(objeto).State = EntityState.Added;
    //            _ctx.Entry(objeto).Property("DataCadastro").CurrentValue = DateTime.Now;
    //            _ctx.SaveChanges();
    //            trans.Commit();

    //            return objeto;
    //        }
    //        catch (Exception err)
    //        {
    //            trans.Rollback();
    //            throw err;
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        _ctx.Dispose();
    //        GC.SuppressFinalize(this);
    //    }
    //}
}
