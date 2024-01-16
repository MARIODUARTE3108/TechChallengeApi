using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Application.Models;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Application.Contracts
{
    public interface INoticiaApplicationService : IDisposable
    {
        Task<Noticia> Inserir(Noticia noticia);
        Task<Noticia> BuscarPorId(int id);
        Task<ICollection<Noticia>> ListarTudo();
        Task<NoticiaModel> Enviar(NoticiaModel noticia);
    }
}
