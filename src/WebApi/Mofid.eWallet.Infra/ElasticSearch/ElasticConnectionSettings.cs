using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.Infra.ElasticSearch
{
    public class ElasticConnectionSettings
    {
        public string ClusterUrl { get; set; }

        public string DefaultIndex
        {
            get
            {
                return this.defaultIndex;
            }
            set
            {
                this.defaultIndex = value.ToLower();
            }
        }

        private string defaultIndex;
    }
}

