using System.Threading.Tasks;

namespace DotNet.Rastreio.Search.App.Models
{
    public interface IRastreioSearch
    {
        Task<ResponseRastreio> GetObjetoRastreioAsync(string codigoRastreio);

        ResponseRastreio GetObjetoRastreio(string codigoRastreio);
    }
}
