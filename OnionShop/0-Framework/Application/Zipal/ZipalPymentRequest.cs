using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application.Zipal
{
    public class ZipalPymentRequest
    {
        public string  merchant{ get; set; }
        public long amount { get; set; }
        public string callbackUrl { get; set; }
        public string description { get; set; }
        public string orderId { get; set; }
        public string mobile { get; set; }
        public List<string> allowedCards { get; set; }
        public string ledgerId { get; set; }
        public string nationalCode { get; set; }
    }
}
