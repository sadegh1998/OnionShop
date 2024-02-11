using _0_Framework.Domain;
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
