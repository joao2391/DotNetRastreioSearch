using DotNet.Rastreio.Search.App.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Playwright;
using System.ComponentModel;


namespace DotNet.Rastreio.Search.App
{
    /// <summary>
    /// Rastreio Search
    /// </summary>
    public class RastreioSearch : BaseRastreioSearch, IRastreioSearch
    {
        public RastreioSearch()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
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


        private async Task<ResponseRastreio> GetRastreioInfoByCodigoRastreio(string codigoRastreio)
        {
            
            //await TestSignalR();
            await TestPlaywright();

            var url = $"https://www.linkcorreios.com.br/?id={codigoRastreio}";

            var responseRastreio = new ResponseRastreio
            {
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


        private async Task TestSignalR()
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://chat.3corp.com.br/correios/chatHub")
                .Build();

            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                
            });

            try
            {
                await connection.StartAsync();
                var testObj = new Teste
                {
                    botid = "5f777949a7038a3c84aacc56",
                    channelid = null,
                    date = "0001-01-01T00:00:00",
                    email = null,
                    id = null,
                    name = "Teste",
                    phone = null,
                    serviceid = null,
                    vars = new object()

                };
                var dados = "";
                var element = "";
                var msg = "AA123456785BR";
                await connection.InvokeAsync("InitChat",testObj);   
                //await connection.InvokeAsync("SendOption", dados, element);
                //await connection.InvokeAsync("SendMessage", dados, msg);
                //connection.On<string, string>("InMessage", (user, message) => {
                //    Console.WriteLine("AAAA");
                //});
            }
            catch (Exception ex)
            {

            }

        }

        private async Task TestPlaywright()
        {
            // var exitCode = Microsoft.Playwright.Program.Main(new[] {"install"});
            // if (exitCode != 0)
            // {
            //     throw new Exception($"Playwright exited with code {exitCode}");
            // }
            var codigoRastreio = "AA123456785BR";
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new()
            {
                Headless = false,
                SlowMo = 100
            });
            var page = await browser.NewPageAsync();
            var resp = await page.GotoAsync("https://www.correios.com.br/home-page-2024/rastreamento");
            //var body = await resp.TextAsync();
            await Task.Delay(200);
            
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            await page.Locator("a[id='btnCookie']").ClickAsync();
            
            await Task.Delay(200);
            
            var div = page.Locator("img[class='webchat_closed']");
            var xpto = await div.TextContentAsync();
            await div.ClickAsync();
            
            //await page.WaitForSelectorAsync("div[class='incoming']");
            var btn = page.Locator("button", new PageLocatorOptions { HasTextString = "Acompanhar meu Objeto" });
            await btn.ClickAsync();

            await Task.Delay(1000);

            await page.WaitForSelectorAsync("div[class='bubble']");

            await Task.Delay(500);
            
            var msg = page.Locator("div", new PageLocatorOptions { HasTextString = "Qual o código de rastreamento do objeto postal?" });
            
            await Task.Delay(5000);
            
            await page.WaitForSelectorAsync("div[class='bubble']");

            await page.FillAsync("input[id='messageInput']", codigoRastreio);
            
            await page.Locator("input[id='messageInput']").PressAsync("Enter");

            await page.WaitForSelectorAsync("div[class='bubble']");
            
            await Task.Delay(5000);

             var allLoc = await page.Locator("div[class='bubble']").AllAsync();
            
            var status = await allLoc[8].TextContentAsync();
            // var enviaMsg = page.Locator("button[id='sendButton']");

            // await enviaMsg.ClickAsync();

            var abc = "";

        }
    }

    public class Teste
    {
        public string id { get; set; }
        public string date { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string serviceid { get; set; }
        public string channelid { get; set; }
        public string botid { get; set; }
        public object vars { get; set; }

    }
}
