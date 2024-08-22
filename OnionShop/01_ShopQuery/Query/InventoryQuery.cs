using _01_ShopQuery.Contracts.Inventory;
using InventoryManagement.InfrastructureEFCore;
using ShopManagement.Infrastracture.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_ShopQuery.Query
{
    public class InventoryQuery : IInventoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;

        public InventoryQuery(ShopContext shopContext, InventoryContext inventoryContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
        }

        public StockStatus CheckStock(IsInStock command)
        {
            var inventory = _inventoryContext.Inventories.FirstOrDefault(x => x.ProductId == command.ProductId);
            if (inventory == null || inventory.CalculateCurrentInventoryStock() < command.Count)
            {
                var product = _shopContext.Products.Select(x => new { x.Id, x.Name }).FirstOrDefault(x=>x.Id == command.ProductId);
                return new StockStatus
                {
                    InStock = false,
                    ProductName = product?.Name
                };
            }

            return new StockStatus { InStock = true };
        }
    }
}
