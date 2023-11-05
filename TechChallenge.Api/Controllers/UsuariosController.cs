using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Application.Contracts;
using TechChallenge.Application.Models;

namespace TechChallenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioApplicationService _usuarioService;

        public UsuariosController(IUsuarioApplicationService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> CadatrarUsuario(UsuarioModel model)
        {
            try
            {
                var usuario = await _usuarioService.Inserir(model);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
       
        }

        [HttpGet]
        public IActionResult BuscarUsuarios()
        {
            var usuarios = _usuarioService.ListarTudo();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarUsuarioPorId(int id)
        {
            var usuarios = _usuarioService.BuscarPorId(id);
            return Ok(usuarios);
        }
    }
}
