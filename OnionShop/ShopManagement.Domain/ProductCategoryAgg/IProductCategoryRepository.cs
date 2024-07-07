using _0_Framework.Domain;
using ShopManagement.ApplicationContract.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository:IRepository<long,ProductCategory>
    {
        string GetSlugby(long id);
        List<ProductCategoryViewModel> GetProductCategories();
        List<ProductCategory> Search(string name);


    }
}
