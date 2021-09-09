using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.Entities.BusinessObjects
{
    public class TemporaryClient
    {
        public string Id { get; set; }
        public string NationalCode { get; set; }
        public bool IsLegal { get; set; }
        public string Mobile { get; set; }
    }
}
