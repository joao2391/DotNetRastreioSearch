using DotNet.Rastreio.Search.App.Models;
using DotNet.Rastreio.Search.App.Utils;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace DotNet.Rastreio.Search.App
{
    /// <summary>
    /// Rastreio Search
    /// </summary>
    public class RastreioSearch : BaseRastreioSearch, IRastreioSearch
    {
        public RastreioSearch()
        {
            ServicePointManager.ServerCertificateValidationCallback =  delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
            { 
                return true; 
            };
        }
        /// <summary>
        /// Retorna o Status do objeto rastreado
        /// </summary>
        /// <param name="codigoRastreio">Código de Rastreio</param>
        /// <returns>JSON contendo todas as informações</returns>
        public async Task<ResponseRastreio> GetObjetoRastreioAsync(string codigoRastreio)
        {
            
            try
            {
                HashSet<ResponseRastreio> hsReponseRastreio = new HashSet<ResponseRastreio>();

                var rastreioInfo = await GetRastreioInfoByCodigoRastreio(codigoRastreio).ConfigureAwait(false);

                return rastreioInfo;

            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (HtmlWebException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }


        }

        /// <summary>
        /// Retorna o Status do objeto rastreado
        /// </summary>
        /// <param name="codigoRastreio">Código de Rastreio</param>
        /// <returns>JSON contendo todas as informações</returns>
        public ResponseRastreio GetObjetoRastreio(string codigoRastreio)
        {

            try
            {
                HashSet<ResponseRastreio> hsReponseRastreio = new HashSet<ResponseRastreio>();

                var rastreioInfo = GetRastreioInfoByCodigoRastreio(codigoRastreio).Result;

                return rastreioInfo;

            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (HtmlWebException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private async Task<ResponseRastreio> GetRastreioInfoByCodigoRastreio(string codigoRastreio){

            var url = $"https://www.linkcorreios.com.br/?id={codigoRastreio}";

            var responseRastreio = new ResponseRastreio{
                History = new List<Info>()
            };

            var resp = await _client.GetAsync(url).ConfigureAwait(false);

            var html = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var listaStatus = doc.DocumentNode.SelectNodes("//ul[@class='linha_status']");

            for (int i = 0; i < listaStatus.Count; i++)
            {
                var split = listaStatus[i].ChildNodes[3].InnerText.Split('|');

                var info = new Info
                {
                    Cidade = listaStatus[i].ChildNodes[5].InnerText[7..],
                    Data = split[0].Trim()[8..],
                    Hora = split[1].Trim()[6..],
                    Status = listaStatus[i].ChildNodes[1].InnerText[8..]
                };

                responseRastreio.History.Add(info);
            }

            return responseRastreio;

        }
    }
}
