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
    }
}
