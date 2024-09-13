using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application.Zipal
{
    public class ZipalPaymentResponse
    {
        public long trackId { get; set; }
        public long result { get; set; }
        public string messagew { get; set; }
    }
}
