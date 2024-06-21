using _0_Framework.Application;
using _01_ShopQuery.Contracts.ProductCategory;
using InventoryManagement.InfrastructureEFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastracture.EFCore;

namespace _01_ShopQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _shopContext.ProductCategories.Select(c => new ProductCategoryQueryModel {
            
            Id = c.Id,
            Name = c.Name,
            Picture = c.Picture,
            PictureAlt = c.PictureAlt,
            PictureTitle = c.PictureTitle,
            Slug = c.Slug
            }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProduct()
        {
            var inventory = _inventoryContext.Inventories.Select(x => new { x.ProductId, x.UnitPrice }).ToList();
            var categories = _shopContext.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x=>x.Category)
                .Select(x=> new ProductCategoryQueryModel
                {
                    Id = x.Id ,
                    Name = x.Name,
                    Products = MapProducts(x.Products)
                }).ToList();

            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    product.Price = inventory.FirstOrDefault(x => x.ProductId == product.Id)?.UnitPrice.ToMoney();
                }
            }

            return categories;
        }

        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products.Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Category = product.Category.Name,
                Slug = product.Slug
            }).ToList();
            
        }
    }
}
