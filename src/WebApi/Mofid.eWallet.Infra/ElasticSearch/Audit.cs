using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.Infra.ElasticSearch
{
    public class Audit
    {
        public DateTime DateTime { get; set; }
        public string Request { get; set; }
        public string Ip { get; set; }
        public string Username { get; set; }
        public string ActionType { get; set; }
        public double ResponseTime { get; set; }
        public int ResponseStatusCode { get; set; }
        public string Response { get; set; }
        public string  ReferenceCode { get; set; }
    }
}
