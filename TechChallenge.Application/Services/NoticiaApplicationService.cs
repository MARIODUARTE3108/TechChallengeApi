using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Application.Contracts;
using TechChallenge.Application.Models;
using TechChallenge.Domain.Contracts.Services;
using TechChallenge.Domain.Entities;
using TechChallenge.Infrastructure.Migrations;

namespace TechChallenge.Application.Services
{
    public class NoticiaApplicationService : INoticiaApplicationService
    {
        private readonly INoticiaDomainService _noticiaDomainService;
        private IMapper _mapper;
        private readonly IAzureBlobApplicationService _azureBlob;

        public NoticiaApplicationService(INoticiaDomainService noticiaDomainService, IMapper mapper, IAzureBlobApplicationService azureBlob)
        {
            _noticiaDomainService = noticiaDomainService;
            _mapper = mapper;
            _azureBlob = azureBlob;
        }
        public Task<Domain.Entities.Noticia> Inserir(NoticiaModel model)
        {
            Domain.Entities.Noticia noticia = _mapper.Map<Domain.Entities.Noticia>(model);
            return _noticiaDomainService.Inserir(noticia);
        }

        public Task<Domain.Entities.Noticia> BuscarPorId(int id)
        {
            return _noticiaDomainService.BuscarPorId(id);
        }

        public Task<ICollection<Domain.Entities.Noticia>> ListarTudo()
        {
            return _noticiaDomainService.ListarTudo();
        }

        public void Dispose()
        {
            _noticiaDomainService.Dispose();
        }
    }
}
