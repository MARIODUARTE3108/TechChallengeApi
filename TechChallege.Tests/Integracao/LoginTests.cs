using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechChallege.Tests.Helper;
using TechChallenge.Api.Settings;
using TechChallenge.Application.Models;

namespace TechChallege.Tests.Integracao
{
    public class LoginTests
    {
        private readonly string _url;
        public LoginTests()
        {
            _url = "/api/Auths/Login";
            var servico = new ServiceCollection();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            servico.AddSingleton<IConfiguration>(config);
            AppSettings.ConnectionStrings = config.GetConnectionString("UsuarioConnection");
        }
        [Fact]
        public async Task Login_Post_Retun_Ok()
        {
            var usuario = await new UsuarioTests().Post_Retun_Ok();
            var model = new LoginModel
            {
                Email = usuario.Email,
                Senha = usuario.Senha
            };
            var client = TestsHelper.CreateClient();
            var result = await client.PostAsync(_url, TestsHelper.CreateContent(model));

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Login_Post_BadRequest()
        {
            var faker = new Faker("pt_BR");
            var model = new LoginModel
            {
                Email = faker.Person.Email,
                Senha = faker.Internet.Password(8)
            };
            var client = TestsHelper.CreateClient();
            var result = await client.PostAsync(_url, TestsHelper.CreateContent(model));

            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
