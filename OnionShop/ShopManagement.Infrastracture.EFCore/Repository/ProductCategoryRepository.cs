using _0_Framework.Infrstructure;
using ShopManagement.ApplicationContract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastracture.EFCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<long , ProductCategory>,IProductCategoryRepository
    {
        private readonly ShopContext _shopContext;

        public ProductCategoryRepository(ShopContext shopContext) : base(shopContext) 
        {
            _shopContext = shopContext;
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _shopContext.ProductCategories.Select(x=> new ProductCategoryViewModel {
           Id = x.Id,   
           Name = x.Name,
            }).ToList();    
        }

        public string GetSlugby(long id)
        {
            return _shopContext.ProductCategories.FirstOrDefault(x => x.Id == id).Slug;
        }

        public List<ProductCategory> Search(string name)
        {
            var productCategory = _shopContext.ProductCategories.Select(x=>x);

            if (!string.IsNullOrWhiteSpace(name))
            {
                productCategory = productCategory.Where(x => x.Name.Contains(name));
            }

            return productCategory.OrderByDescending(x=>x.Id).ToList();
        }
    }
}
