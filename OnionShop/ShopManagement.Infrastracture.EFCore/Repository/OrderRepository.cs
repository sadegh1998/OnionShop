using _0_Framework.Infrstructure;
using Microsoft.EntityFrameworkCore;
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

        public OrderRepository(ShopContext shopContext) : base(shopContext)
        {
            _shopContext = shopContext;
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
    }
}
