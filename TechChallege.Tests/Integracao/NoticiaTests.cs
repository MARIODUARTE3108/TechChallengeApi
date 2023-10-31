using Bogus;
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

namespace TechChallege.Tests.Integracao
{
    public class NoticiaTests
    {
        private readonly string _url;
        private static string _token => "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6IkFudGhvbnkuU2FyYWl2YTUzQGJvbC5jb20uYnIiLCJleHAiOjE2OTg3NzU2NzR9.gloSNbx5t1GOIrdbqDNKReecUYCvLOOWaxEY3y8dsgQ";

        public NoticiaTests()
        {
            _url = "/api/Noticias";
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
