using DotNet.Rastreio.Search.App;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;
using Moq;
using DotNet.Rastreio.Search.App.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DotNet.Rastreio.Search.Tests
{
    public class RastreioSearchTests
    {

        IRastreioSearch _rastreio;
        string codigoRastreio = "AA123456785BR";
        string mockHtml = "";
        ResponseRastreio _responseRastreio;

        [SetUp]
        public void Setup()
        {
            _responseRastreio = new ResponseRastreio{History = []};
            _responseRastreio.History.Add(new Info
                {
                    Cidade = "Unidade de Distribuição - Sao Paulo / SP", 
                    Data = "27/06/2024",
                    Hora = "Hora: 14:01",
                    Status = "Objeto entregue ao destinatário"
                }
            );
        }

        [Test]
        public async Task Should_Return_Non_Empty_String_Async()
        {
            var mock = new Mock<IRastreioSearch>();
            var mockResponse = JsonConvert.SerializeObject(_responseRastreio);
            mock.Setup(x => x.GetObjetoRastreioAsync(codigoRastreio)).ReturnsAsync(mockResponse);
            _rastreio = mock.Object;

            var result = await _rastreio.GetObjetoRastreioAsync(codigoRastreio);

            Assert.IsTrue(!string.IsNullOrEmpty(result));
        }

        [Test]
        public void Should_Return_Non_Empty_String()
        {
            var mock = new Mock<IRastreioSearch>();
            var mockResponse = JsonConvert.SerializeObject(_responseRastreio);
            mock.Setup(x => x.GetObjetoRastreio(codigoRastreio)).Returns(mockResponse);
            _rastreio = mock.Object;

            var result = _rastreio.GetObjetoRastreio(codigoRastreio);

            Assert.IsTrue(!string.IsNullOrEmpty(result));
        }
    }
}