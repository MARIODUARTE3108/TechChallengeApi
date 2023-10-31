using Bogus;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechChallege.Tests.Helper;
using TechChallenge.Application.Models;
using TechChallenge.Domain.Entities;

namespace TechChallege.Tests.Integracao
{
    public class UsuarioTests
    {

        private readonly string _url;

        public UsuarioTests()
        {
            _url = "/api/Usuarios";
        }

        [Fact]
        public async Task<Usuario> Post_Retun_Ok()
        {
            var faker = new Faker("pt_BR");

            var senha = "Du@rte3108+";

            var model = new UsuarioModel
            {
                Nome = faker.Person.FullName,
                Email = faker.Person.Email,
                Senha = senha,
                SenhaConfirmacao = senha
            };
            var client = TestsHelper.CreateClient();
            var result = await client.PostAsync(_url, TestsHelper.CreateContent(model));

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            var usuario = TestsHelper.CreateResponse<Usuario>(result);

            usuario.Senha = senha;
            return usuario;

        }
        [Fact]
        public async void Post_Retun_BadRequest()
        {
            var usuario = await Post_Retun_Ok();

            var model = new UsuarioModel
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha,
            };

            var client = TestsHelper.CreateClient();
            var result = await client.PostAsync(_url, TestsHelper.CreateContent(model));
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
