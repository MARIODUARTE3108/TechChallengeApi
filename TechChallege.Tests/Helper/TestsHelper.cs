using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallege.Tests.Helper
{
    public class TestsHelper
    {
        public static HttpClient CreateClient()
        {
            var application = new WebApplicationFactory<Program>();
            return application.CreateClient();
        }
        public static StringContent CreateContent<TRequest>(TRequest request)
        {
            return new StringContent(JsonConvert.SerializeObject(request),
                Encoding.UTF8, "application/json");
        }
        public static TResponse CreateResponse<TResponse>(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
