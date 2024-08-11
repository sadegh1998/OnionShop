using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShopManagement.ApplicationContract.Order;
using ShopManagement.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;

        public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _authHelper = authHelper;
            _configuration = configuration;
        }


        public long PlaceOrder(Cart cart)
        {
            var accountId = _authHelper.CurrentAccountId();
            var order = new Order(accountId,cart.Amount,cart.DiscountAmount,cart.PayAmount,cart.PaymentMethod);

            foreach (var cartItem in cart.Items)
            {
                var orderItem = new OrderItem(cartItem.Id, cartItem.UnitPrice, cartItem.DiscountRate, cartItem.Count);
                order.AddItem(orderItem);
            }
            _orderRepository.Create(order);
            _orderRepository.SaveChanges();
            return order.Id;
        }

        public string PaymentSucceeded(long orderId, long refId)
        {
            var order = _orderRepository.Get(orderId);
            order.PaymentSucceded(refId);
            var symbol = _configuration["Symbol"];
            var issueTrackingNo = CodeGenerator.Generate(symbol);
            order.SetIssueTrackingNo(issueTrackingNo);
            _orderRepository.SaveChanges();
            return issueTrackingNo;
        }

        public double GetAmountBy(long id)
        {
            return _orderRepository.GetAmountBy(id);
        }
    }
}
