using System.Collections.Generic;

namespace DotNet.Rastreio.Search.App.Models
{
    public class ResponseRastreio
    {
        public List<Info> History { get; set; }
    }

    public class Info
    {
        public string Data { get; set; }

        public string Hora {get; set; }

        public string Cidade { get; set; }

        public string Status { get; set; }
    }
}
