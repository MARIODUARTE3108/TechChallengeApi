using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Api.Settings;
using TechChallenge.Application.Contracts;
using TechChallenge.Application.Models;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(AuthenticationSchemes = "Bearer")]
    public class NoticiasController : ControllerBase
    {
        private readonly INoticiaApplicationService _noticiaAppService;
        private readonly IBus _bus;

        public NoticiasController(INoticiaApplicationService noticiaAppService, IBus bus)
        {
            _noticiaAppService = noticiaAppService;
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarNoticia(NoticiaModel noticia)
        {
            try
            {
                var endpont = await _bus.GetSendEndpoint(new Uri($"queue:{AppSettings.NomeFilaServiceBus}"));
                await endpont.Send(noticia);
                return StatusCode(200, new { message = "Notícia cadastrada com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuscarNoticias()
        {
            var noticia = await _noticiaAppService.ListarTudo();
            return StatusCode(200, noticia);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarNoticiaPorId(int id)
        {
            var noticia = await _noticiaAppService.BuscarPorId(id);
            return Ok(noticia);
        }
    }
}
