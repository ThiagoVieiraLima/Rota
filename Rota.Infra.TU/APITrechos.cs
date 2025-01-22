using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Rota.Servicos.API;
using Xunit;

namespace Rota.Infra.TU
{
    public class APITrechos
    {
        private readonly HttpClient _client;

        public APITrechos()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.TU.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();

            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>()
                .UseConfiguration(builder.Build())
                );
            
            _client = server.CreateClient();

            //Carga Inicial
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Trecho/CargaInicial");
            var response = _client.SendAsync(request).Result;
        }

        [Fact]
        public void A01_Incluir_Http200()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Trecho");

            //Act
            var data = new Rota.Aplicacao.DTO.TrechoDTO() 
            { Id = 100, Origem = "TE1", Destino = "TE2", Valor = 12.312 };

            request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void A02_Incluir_Http400()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Trecho");

            //Act
            var data = new Rota.Aplicacao.DTO.TrechoDTO()
            { Id = 200, Origem = "ORIGEM", Destino = "DESTINO", Valor = 1234567.312 };

            request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public void A03_Listar_Http200()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Trecho");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void A05_SelecionarPorId_Http200()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Trecho/1");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void A06_SelecionarPorId_Http404()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Trecho/300");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public void A07_Alterar_Http200()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Put, "/api/Trecho");

            //Act
            var data = new Rota.Aplicacao.DTO.TrechoDTO()
            { Id = 2, Origem = "TE2", Destino = "TE3", Valor = 12.312 };

            request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void A08_Alterar_Http400()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Put, "/api/Trecho");

            //Act
            var data = new Rota.Aplicacao.DTO.TrechoDTO()
            { Id = 3, Origem = "ORIGEM", Destino = "DESTINO", Valor = 1234567.312 };

            request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public void A08_Alterar_Http404()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Put, "/api/Trecho");

            //Act
            var data = new Rota.Aplicacao.DTO.TrechoDTO()
            { Id = 300, Origem = "DES", Destino = "ORI", Valor = 123456.312 };

            request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public void A09_Excluir_Http200()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/Trecho/4");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
