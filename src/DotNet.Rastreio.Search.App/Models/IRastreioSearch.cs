using System.Threading.Tasks;

namespace DotNet.Rastreio.Search.App.Models
{
    public interface IRastreioSearch
    {
        Task<string> GetObjetoRastreioAsync(string codigoRastreio);

        string GetObjetoRastreio(string codigoRastreio);
    }
}
