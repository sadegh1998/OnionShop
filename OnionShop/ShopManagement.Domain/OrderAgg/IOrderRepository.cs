﻿using _0_Framework.Domain;
using ShopManagement.ApplicationContract.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IOrderRepository : IRepository<long,Order>
    {
        double GetAmountBy(long id);
        List<OrderItemViewModel> GetItems(long orderId);
        List<OrderviewModel> Search(OrderSearchModel searchModel);
    }
}
