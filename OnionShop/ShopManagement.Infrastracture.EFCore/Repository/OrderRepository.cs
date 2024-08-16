using _0_Framework.Application;
using _0_Framework.Infrstructure;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.ApplicationContract;
using ShopManagement.ApplicationContract.Order;
using ShopManagement.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastracture.EFCore.Repository
{
    public class OrderRepository : RepositoryBase<long , Order> , IOrderRepository
    {
        private readonly ShopContext _shopContext;
        private readonly AccountContext _accountContext;

        public OrderRepository(ShopContext shopContext, AccountContext accountContext) : base(shopContext)
        {
            _shopContext = shopContext;
            _accountContext = accountContext;
        }

        public double GetAmountBy(long id)
        {
            var payAmount = _shopContext.Orders.Select(x => new { x.PayAmount, x.Id }).FirstOrDefault(x => x.Id == id);
            if(payAmount != null)
            {
                return payAmount.PayAmount;
            }
            return 0;
        }

        public List<OrderItemViewModel> GetItems(long orderId)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name });
            var order = _shopContext.Orders.FirstOrDefault(x => x.Id == orderId);
            if(order == null)
            {
                return new List<OrderItemViewModel>();
            }

            var items = order.Items.Select(x => new OrderItemViewModel { 
             Id = x.Id,   
            ProductId = x.ProductId,
            Count = x.Count,
            DiscountRate = x.DiscountRate,
            OrderId = x.OrderId,
            UnitPrice = x.UnitPrice
            }).ToList();

            foreach (var item in items)
            {
                item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            }
            return items;
        }

        public List<OrderviewModel> Search(OrderSearchModel searchModel)
        {
            var account = _accountContext.Accounts.Select(x => new { x.Id, x.FullName }).ToList();
            var query = _shopContext.Orders.Select(x => new OrderviewModel { 
            Id = x.Id,
            AccountId = x.AccountId,
            TotalDiscount = x.TotalDiscount,
            IsCanceled = x.IsCanceled,
            IsPaid = x.IsPaid,
            IssueTrackingNo = x.IssueTrackingNo,
            TotalPrice = x.TotalPrice,
            PaymentMethodId = x.PaymentMethod,
            PayAmount = x.PayAmount,
            CreationDate = x.CreationDate.ToFarsi(),
            RefId = x.RefId

            });

            query = query.Where(x => x.IsCanceled == searchModel.IsCancel);

            if(searchModel.AccountId > 0)
            {
                query = query.Where(x => x.AccountId == searchModel.AccountId);
            }
            var orders = query.OrderByDescending(x=>x.Id).ToList();

            foreach (var order in orders)
            {
                order.AccountFullName = account.FirstOrDefault(x => x.Id == order.AccountId)?.FullName;
                order.PaymentMethod = PaymentMethod.GetBy(order.PaymentMethodId);
            }

            return orders;

        }
    }
}
