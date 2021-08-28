using System.Threading.Tasks;

namespace DotNet.Rastreio.Search.App.Models
{
    internal interface IRastreioSearch
    {
        Task<string> GetObjetoRastreioAsync(string codigoRastreio);

        string GetObjetoRastreio(string codigoRastreio);
    }
}
