using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application.Zipal
{
    public class ZipalFactory : IZipalFactory
    {
        private readonly IConfiguration _configuration;
        private string MerchantId;
        private string SiteUrl;
        public string Prefix { get; set; }

        public ZipalFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            MerchantId = _configuration.GetSection("paymentZipal")["merchant"];
            SiteUrl = _configuration.GetSection("paymentZipal")["siteUrl"];
            Prefix = _configuration.GetSection("paymentZipal")["perfix"];
        }

        public ZipalPaymentResponse CreatePaymentRequest(string amount, string description, string orderId, string mobile, string ledgerId, string nationalCode)
        {
            amount = amount.Replace(',', ' ');
            var finalAmount = long.Parse(amount);
            var client = new RestClient($"{Prefix}/v1/request");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = new ZipalPymentRequest { 
            merchant = MerchantId,
            amount = finalAmount,
             description = description,
             mobile = mobile,
             orderId = orderId,
             nationalCode = nationalCode,
             callbackUrl = $"{SiteUrl}/Checkout?handler=CallBack",
             ledgerId  = ledgerId,
             allowedCards = new List<string>()
            };
            request.AddJsonBody(body);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<ZipalPaymentResponse>(response.Content);
        }

        public ZipalVerificationResponse CreateVerificationRequest(long trackId)
        {
            var client = new RestClient($"{Prefix}/v1/verify");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = new ZipalVerificationRequest
            {
               merchant = MerchantId,
               trackId = trackId
            };
            request.AddJsonBody(body);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<ZipalVerificationResponse>(response.Content);
        }
    }
}
