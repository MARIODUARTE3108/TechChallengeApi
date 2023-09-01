using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Domain.Entities;
using TechChallenge.Infrastructure.Mappings;

namespace TechChallenge.Infrastructure.Context
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> dbContextOptions)
           : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NoticiaMap());
              modelBuilder.ApplyConfiguration(new UsuarioMap());
        }

        public DbSet<Noticia>? Noticia { get; set; }
        public DbSet<Usuario>? Usuario { get; set; }
    }
}

