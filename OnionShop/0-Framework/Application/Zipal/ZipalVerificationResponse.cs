using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application.Zipal
{
    public class ZipalVerificationResponse
    {
        public DateTime paidAt { get; set; }
        public long amount { get; set; }
        public long Result { get; set; }
        public long status { get; set; }
        public string refNumber { get; set; }
        public string description { get; set; }
        public string cardNumber { get; set; }
        public string orderId { get; set; }
        public string message { get; set; }
    }
}
