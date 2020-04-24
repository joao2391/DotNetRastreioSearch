using DotNet.Rastreio.Search.App.Utils;
using System;
using System.Net.Http;

namespace DotNet.Rastreio.Search.App.Models
{
    public abstract class BaseRastreioSearch
    {
        protected HttpClient _client;

        public BaseRastreioSearch()
        {
            _client = new HttpClient()
            {
                Timeout = TimeSpan.FromMinutes(30)
            };
        }

        protected string UrlCorreio { get => Constants.URL_CORREIO; }
    }
}
