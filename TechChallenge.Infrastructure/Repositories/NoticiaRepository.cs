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
    public class NoticiaRepository : BaseReposiitory<Noticia>, INoticiaRepository
    {
        protected readonly SqlServerContext contexto;


        public NoticiaRepository(SqlServerContext sqlServerContext) : base(sqlServerContext)
        {
            contexto = sqlServerContext;
        }
    }
}
