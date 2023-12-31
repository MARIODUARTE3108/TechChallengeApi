﻿using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechChallege.Tests.Helper;
using TechChallenge.Application.Models;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Api.Settings;

namespace TechChallege.Tests.Integracao
{
    public class NoticiaTests
    {
        private readonly string _url;
        private static string _token => "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImR1YXJ0ZTMxMDA4QGdtYWlsLmNvbSIsImV4cCI6MTcwMTUzMDMzNn0.ERizjc_oagb_XlSTTj_OnoTUJctDxY7IFy0x9zmMH9k";

        public NoticiaTests()
        {
            _url = "/api/Noticias";
            var servico = new ServiceCollection();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            servico.AddSingleton<IConfiguration>(config);
            AppSettings.ConnectionStrings = config.GetConnectionString("UsuarioConnection");
        }

        [Fact]
        public async Task Noticia_Post_Retun_Ok()
        {
            var faker = new Faker("pt_BR");
            var model = new NoticiaModel()
            {
                Titulo = faker.Lorem.Sentence(),
                Descricao = faker.Lorem.Paragraphs(3),
                Chapeu = faker.Lorem.Sentence(),
                DataPublicacao = DateTime.Now,
                Autor = faker.Person.FirstName
            };
            var client = TestsHelper.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var result = await client.PostAsync(_url, TestsHelper.CreateContent(model));

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Noticia_Post_BadRequest()
        {
            var faker = new Faker("pt_BR");
            var model = new NoticiaModel
            {
                Descricao = faker.Lorem.Paragraphs(3),
                Chapeu = faker.Lorem.Sentence(),
                DataPublicacao = DateTime.Now,
                Autor = faker.Person.FirstName
            };
            var client = TestsHelper.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var result = await client.PostAsync(_url, TestsHelper.CreateContent(model));

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
