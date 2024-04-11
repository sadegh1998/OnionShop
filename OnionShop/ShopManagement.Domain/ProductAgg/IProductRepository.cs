using _0_Framework.Domain;
using ShopManagement.ApplicationContract.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.ProductAgg
{
   public interface  IProductRepository : IRepository<long , Product>
    {
        EditProduct GetDetails(long id);
        List<ProductViewModel> GetProducts();

        List<ProductViewModel> Search(ProductSearchModel searchModel);
    }
}
