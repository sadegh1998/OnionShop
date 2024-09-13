using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application.Zipal
{
    public interface IZipalFactory
    {
        string Prefix { get; set; }

        ZipalPaymentResponse CreatePaymentRequest(string amount , string description , string orderId,string mobile,string ledgerId , string nationalCode);
        ZipalVerificationResponse CreateVerificationRequest( long trackId);
    }
}
