using DotNet.Rastreio.Search.App.Utils;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace DotNet.Rastreio.Search.App.Models
{
    public abstract class BaseRastreioSearch
    {
        protected HttpClient _client;

        public BaseRastreioSearch()
        {
            ServicePointManager.ServerCertificateValidationCallback =  delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
            { 
                return true; 
            };

            _client = new HttpClient()
            {
                Timeout = TimeSpan.FromMinutes(30)
            };
        }

        protected string UrlCorreio { get => Constants.URL_CORREIO; }
    }
}
