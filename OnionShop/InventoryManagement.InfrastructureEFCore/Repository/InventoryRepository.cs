using _0_Framework.Domain;
using InventoryManagement.ApplicationContract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastracture.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.InfrastructureEFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;

        public InventoryRepository(InventoryContext inventoryContext, ShopContext shopContext) : base(inventoryContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }

        public Inventory GetBy(long producId)
        {
            return _inventoryContext.Inventories.FirstOrDefault(x => x.ProductId == producId);
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryContext.Inventories.Select(x=> new EditInventory {
            Id = x.Id ,
            ProductId = x.ProductId,
            UnitPrice = x.UnitPrice
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new {x.Id ,x.Name }).ToList();
            var query = _inventoryContext.Inventories.Select(x=> new InventoryViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                 InStock= x.InStock,
                 UnitPrice = x.UnitPrice,
                 CurrentCount = x.CalculateCurrentInventoryStock()
            });

            if(searchModel.ProductId != 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            if (!searchModel.InStock)
            {
                query = query.Where(x => !x.InStock);
            }
            
            var inventories = query.OrderByDescending(x => x.Id).ToList();
            inventories.ForEach(x => x.Product = products.FirstOrDefault(item => item.Id == x.ProductId)?.Name);
            return inventories;
        }
    }
}
