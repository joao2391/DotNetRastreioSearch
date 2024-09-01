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
        public async Task<string> GetObjetoRastreioAsync(string codigoRastreio)
        {
            HashSet<ResponseRastreio> hsReponseRastreio = new HashSet<ResponseRastreio>();

            await GetRastreioInfoByCodigoRastreio(codigoRastreio);

            try
            {
                

                // Dictionary<string, string> dict = new Dictionary<string, string>
                // {
                //     { "acao", "track" },
                //     { "objetos", codigoRastreio },
                //     { "btnPesq", "buscar" }
                // };

                // var req = new HttpRequestMessage(HttpMethod.Post, Constants.URL_CORREIO) { Content = new FormUrlEncodedContent(dict) };
                // var rep = await _client.SendAsync(req).Result.Content.ReadAsStringAsync();
                // var html = rep;

                // var doc = new HtmlDocument();
                // doc.LoadHtml(html);
                // var ultimoStatus = doc.DocumentNode.SelectNodes("/html/body/div[1]/div[3]/div[2]/div/div/div[2]/div[2]/div[4]/div[1]/div[2]");//TODO
                // var tabela = doc.DocumentNode.SelectNodes("//table[@class='listEvent sro']");

                // if (tabela == null)
                // {
                //     return Constants.NOT_FOUND_MESSAGE;
                // }

                // string status = ultimoStatus[0].ChildNodes[2].InnerText;
                // string data = ultimoStatus[0].ChildNodes[4].InnerText.Replace("\r\n", "").Substring(0, 16);
                // string cidade = ultimoStatus[0].ChildNodes[4].InnerText.Replace("\r\n", "").Substring(17);

                // ResponseRastreio ultimoRastreio = new ResponseRastreio
                // {
                //     Cidade = cidade,
                //     Data = data,
                //     Status = status
                // };

                // hsReponseRastreio.Add(ultimoRastreio);

                // var terres = tabela[0].SelectNodes("//tr//td");
                // string dataHora = string.Empty;
                // string cidadeEstado = string.Empty;
                // string statusObj = string.Empty;

                // for (int i = 0; i < terres.Count; i++)
                // {
                //     if (i % 2 == 0)
                //     {
                //         dataHora = terres[i].InnerText.Replace("\r\n", "").Substring(0, 16);
                //         cidadeEstado = terres[i].ChildNodes[5].InnerText.Replace("&nbsp", "") == "" ?
                //         terres[i].ChildNodes[4].InnerText.Replace("\r\n", "") : terres[i].ChildNodes[5].InnerText.Replace("&nbsp;", "");
                //         statusObj = terres[i + 1].ChildNodes[1].InnerText;

                //         ResponseRastreio responseRastreio = new ResponseRastreio
                //         {
                //             Cidade = cidade,
                //             Data = data,
                //             Status = status
                //         };

                //         hsReponseRastreio.Add(responseRastreio);
                //     }
                // }

                // return JsonConvert.SerializeObject(hsReponseRastreio);
                return "";

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
        public string GetObjetoRastreio(string codigoRastreio)
        {
            HashSet<ResponseRastreio> hsReponseRastreio = new HashSet<ResponseRastreio>();

            try
            {
                // Dictionary<string, string> dict = new Dictionary<string, string>
                // {
                //     { "acao", "track" },
                //     { "objetos", codigoRastreio },
                //     { "btnPesq", "buscar" }
                // };

                // var req = new HttpRequestMessage(HttpMethod.Post, Constants.URL_CORREIO) { Content = new FormUrlEncodedContent(dict) };
                // var rep =  _client.SendAsync(req).Result.Content.ReadAsStringAsync().Result;
                // var html = rep;

                // var doc = new HtmlDocument();
                // doc.LoadHtml(html);
                // var ultimoStatus = doc.DocumentNode.SelectNodes("/html/body/div[1]/div[3]/div[2]/div/div/div[2]/div[2]/div[4]/div[1]/div[2]");//TODO
                // var tabela = doc.DocumentNode.SelectNodes("//table[@class='listEvent sro']");

                // if (tabela == null)
                // {
                //     return Constants.NOT_FOUND_MESSAGE;
                // }

                // string status = ultimoStatus[0].ChildNodes[2].InnerText;
                // string data = ultimoStatus[0].ChildNodes[4].InnerText.Replace("\r\n", "").Substring(0, 16);
                // string cidade = ultimoStatus[0].ChildNodes[4].InnerText.Replace("\r\n", "").Substring(17);

                // ResponseRastreio ultimoRastreio = new ResponseRastreio
                // {
                //     Cidade = cidade,
                //     Data = data,
                //     Status = status
                // };

                // hsReponseRastreio.Add(ultimoRastreio);

                // var terres = tabela[0].SelectNodes("//tr//td");
                // string dataHora = string.Empty;
                // string cidadeEstado = string.Empty;
                // string statusObj = string.Empty;

                // for (int i = 0; i < terres.Count; i++)
                // {
                //     if (i % 2 == 0)
                //     {
                //         dataHora = terres[i].InnerText.Replace("\r\n", "").Substring(0, 16);
                //         cidadeEstado = terres[i].ChildNodes[5].InnerText.Replace("&nbsp", "") == "" ?
                //         terres[i].ChildNodes[4].InnerText.Replace("\r\n", "") : terres[i].ChildNodes[5].InnerText.Replace("&nbsp;", "");
                //         statusObj = terres[i + 1].ChildNodes[1].InnerText;

                //         ResponseRastreio responseRastreio = new ResponseRastreio
                //         {
                //             Cidade = cidade,
                //             Data = data,
                //             Status = status
                //         };

                //         hsReponseRastreio.Add(responseRastreio);
                //     }
                // }

                return JsonConvert.SerializeObject(hsReponseRastreio);

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


        private async Task<string> GetRastreioInfoByCodigoRastreio(string codigoRastreio){

            var url = $"https://www.linkcorreios.com.br/?id={codigoRastreio}";

            //var hsReponseRastreio = new HashSet<Info>();
            var responseRastreio = new ResponseRastreio{
                History = new List<Info>()
            };

            var resp = await _client.GetAsync(url).ConfigureAwait(false);

            var html = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            //var ultimoStatus = doc.DocumentNode.SelectNodes("//ul[@class='linha_status m-0']//li");
            var listaStatus = doc.DocumentNode.SelectNodes("//ul[@class='linha_status']");
            //var splitDataHora = ultimoStatus[1].InnerText.Split('|');

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

            // var ultimoRastreio = new Info{
            //     Status = ultimoStatus[0].ChildNodes[1].InnerText,
            //     Data = splitDataHora[0].Trim()[8..],
            //     Hora = splitDataHora[1].Trim()[6..],
            //     Cidade = ultimoStatus[2].InnerText[7..]
            // };

            // hsReponseRastreio.Add(ultimoRastreio);

            var rastreioInfo = JsonConvert.SerializeObject(responseRastreio);

            return rastreioInfo;


        }
    }
}
