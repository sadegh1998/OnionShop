using InventoryManagement.ApplicationContract.Inventory;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.InventoryAcl
{
    public class ShopInventoryAcl : IShopInventoryAcl
    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public bool ReduceFromInventory(List<OrderItem> items)
        {
            var command = new List<DecreaseInventory>();
           foreach (var orderItem in items)
            {
                var item = new DecreaseInventory(orderItem.ProductId,orderItem.Count,"by customer",orderItem.OrderId);
                command.Add(item);

            }
            return _inventoryApplication.Reduce(command).IsSuccedded;
        }
    }
}
