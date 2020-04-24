using DotNet.Rastreio.Search.App;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests
{
    public class RastreioSearchTests
    {

        RastreioSearch _rastreio;
        string codigoRastreio = "12515520";

        [SetUp]
        public void Setup()
        {
            _rastreio = new RastreioSearch();
        }

        [Test]
        public async Task Should_Return_Non_Empty_String_Async()
        {
            var result = await _rastreio.GetObjetoRastreioAsync(codigoRastreio);

            Assert.IsTrue(!string.IsNullOrEmpty(result));
        }

        [Test]
        public void Should_Return_Non_Empty_String()
        {
            var result =  _rastreio.GetObjetoRastreio(codigoRastreio);

            Assert.IsTrue(!string.IsNullOrEmpty(result));
        }
    }
}