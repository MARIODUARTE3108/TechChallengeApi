using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using TechChallenge.Api.Settings;
using TechChallenge.Application.Contracts;
using TechChallenge.Application.Services;
using TechChallenge.Domain.Contracts.Datas;
using TechChallenge.Domain.Entities;
using TechChallenge.Infrastructure.Repositories;

namespace TechChallenge.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IQueueClient _queueClient;
        private readonly ServiceBusClient _clientCreateNoticia;
        private readonly INoticiaApplicationService _noticiaAppService;
        public Worker(ILogger<Worker> logger, INoticiaApplicationService noticiaAppService)
        {
            _logger = logger;
            _clientCreateNoticia = new ServiceBusClient(AppSettings.ConnectionStringServiceBus);
            _noticiaAppService = noticiaAppService;
            var servico = new ServiceCollection();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker iniciado: {time}", DateTimeOffset.Now);

                await Task.Run(() => RegisterOnMessageHandlerAndReceiveMessages(stoppingToken));

                await Task.Delay(1000, stoppingToken);
            }
            _queueClient.CloseAsync().Wait();
            _logger.LogInformation("Worker encerrado.");
        }
        private async Task RegisterOnMessageHandlerAndReceiveMessages(CancellationToken cancellationToken)
        {
            var receiveNews = _clientCreateNoticia.CreateReceiver(AppSettings.NomeFilaServiceBus);
            while (!cancellationToken.IsCancellationRequested)
            {
                var messageCreateNewsTask = receiveNews.ReceiveMessageAsync();

                await Task.WhenAny(messageCreateNewsTask);

                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                if (messageCreateNewsTask.IsCompletedSuccessfully)
                {
                    var messageCreateNews = messageCreateNewsTask.Result;
                    if (messageCreateNews != null)
                    {
                        await ProcessCreateMember(receiveNews, messageCreateNews);
                    }
                }

            }
        }
        private async Task ProcessCreateMember(ServiceBusReceiver receiver, ServiceBusReceivedMessage message)
        {
            var messageString = Encoding.UTF8.GetString(message.Body);
            var noticia = JsonConvert.DeserializeObject<Noticia>(messageString);

            if (noticia != null)
            {
                await _noticiaAppService.Inserir(noticia);
                _logger.LogInformation($"Notícia {noticia.Chapeu} criada por {noticia.Autor}, cadastrada com sucesso!");
                await receiver.CompleteMessageAsync(message);
            }
            else
            {
                _logger.LogInformation($"Erro ao cadastrar Notícia!");
                await receiver.AbandonMessageAsync(message);
            }
        }
    }
}
