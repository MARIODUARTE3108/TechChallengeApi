using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SisAgenda.Infra.Seguranca.Servico;
using TechChallenge.Application.Contracts;
using TechChallenge.Application.Models;
using TechChallenge.Domain.Contracts.Services;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Service;

namespace TechChallenge.Application.Services
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {
        private readonly IUsuarioDomainService _usuarioDomainService;
        private IMapper _mapper;
        private TokenApplicationService _tokenService;

        public UsuarioApplicationService(IUsuarioDomainService usuarioDomainService, IMapper mapper, TokenApplicationService tokenService)
        {
            _usuarioDomainService = usuarioDomainService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<Usuario> Inserir(UsuarioModel model)
        {
            var verificaEmail = _usuarioDomainService.BuscarPorEmail(model.Email);

            if (verificaEmail.Result == null)
            {
                Usuario usuario = _mapper.Map<Usuario>(model);

                usuario.Senha = CriptografiaServico.Encrypt(usuario.Senha);
                return await _usuarioDomainService.Inserir(usuario);

            }
            else
            {
                throw new ApplicationException("Email já cadastrado!");
            }
        }

        public async Task<Usuario> BuscarPorId(int id)
        {
            var usuario = await _usuarioDomainService.BuscarPorId(id);

            if (usuario != null)
            {
                return usuario;
            }
            throw new ApplicationException("Usuario não cadastrado");
        }

        public async Task<ICollection<Usuario>> ListarTudo()
        {
            var usuarios = await _usuarioDomainService.ListarTudo();

            return usuarios;
        }

        public string GetAccessToken(LoginModel model)
        {
            var usuario = _usuarioDomainService.BuscarPorEmail(model.Email);
            if (usuario.Result != null)
            {
                var verificaUsuario = _usuarioDomainService.VerificarLogin(usuario.Result.Email, model.Senha);


                if (verificaUsuario.Result != null)
                {
                    var token = _tokenService.GenerateToken(usuario.Result);

                    return token;
                }
                throw new ApplicationException("Usuário não autenticado!");
            }
            throw new ApplicationException("Usuario não cadastrado");
        }

        public void Dispose()
        {
            _usuarioDomainService.Dispose();
        }

        public Task<Usuario> BuscarPorEmail(string email)
        {
            return _usuarioDomainService.BuscarPorEmail(email);
        }
    }
}
