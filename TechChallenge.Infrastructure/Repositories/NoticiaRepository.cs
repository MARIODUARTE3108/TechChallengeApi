using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Domain.Contracts.Datas;
using TechChallenge.Domain.Entities;
using TechChallenge.Infrastructure.Context;

namespace TechChallenge.Infrastructure.Repositories
{
    public class NoticiaRepository : INoticiaRepository
    {
        private readonly SqlServerContext _contexto;

        public NoticiaRepository()
        {
            _contexto = new SqlServerContext();
        }

        public async Task<ICollection<Noticia>> ListarTudo()
        {
            try
            {
                return await _contexto.Noticia.ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Erro ao obter Noticias.");
            }
        }

        public async Task<Noticia> BuscarPorId(int id)
        {
            try
            {
                var agencia = _contexto.Noticia.FirstOrDefault(p => p.Id == id);
                if (agencia == null)
                {
                    return null;
                }
                return agencia;
            }
            catch
            {
                throw new Exception($"Erro ao obter noticias com id = {id}.");
            }
        }

        public async Task<Noticia> Inserir(Noticia noticia)
        {
            try
            {
                noticia.DataCadastro = DateTime.Now;
                _contexto.Noticia.Add(noticia);
                _contexto.SaveChanges();

                return noticia;
            }
            catch
            {
                throw new Exception($"Erro ao cadatrar noticia");
            }
        }

        public async void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
