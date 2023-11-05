using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Application.Contracts;
using TechChallenge.Application.Models;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IUsuarioApplicationService _usuarioService;

        public AuthsController(IUsuarioApplicationService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                var Accesstoken = _usuarioService.GetAccessToken(model);

                if (Accesstoken != null)
                {
                    var result = new
                    {
                        Mensagem = "Autenticação realizada com sucesso.",
                        AccessToken = _usuarioService.GetAccessToken(model),
                        Nome = _usuarioService.BuscarPorEmail(model.Email).Result.Nome,
                    };
                    return Ok(result);
                }
                return Unauthorized("Acesso negado.");
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
            
        }
    }
}
