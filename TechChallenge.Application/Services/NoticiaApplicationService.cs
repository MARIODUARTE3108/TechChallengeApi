using AutoMapper;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Api.Settings;
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

        public NoticiaApplicationService(INoticiaDomainService noticiaDomainService, IMapper mapper)
        {
            _noticiaDomainService = noticiaDomainService;
            _mapper = mapper;
        }
        public Task<Domain.Entities.Noticia> Inserir(Domain.Entities.Noticia model)
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

        public async Task<NoticiaModel> Enviar(NoticiaModel noticia)
        {
            var mensagemJson = JsonConvert.SerializeObject(noticia);
            var mensagem = new ServiceBusMessage(Encoding.UTF8.GetBytes(mensagemJson));

            await using (var serviceBusClient = new ServiceBusClient(AppSettings.ConnectionStringServiceBus))
            {
                var sender = serviceBusClient.CreateSender(AppSettings.NomeFilaServiceBus);
                await sender.SendMessageAsync(mensagem);
            }
            return noticia;
        }

    }
}
