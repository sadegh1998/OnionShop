using _0_Framework.Domain;
using ShopManagement.ApplicationContract.ProductPicture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.ProiductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long , ProductPicture>
    {
        ProductPicture GetProductAndCategoryBy(long id);
        EditProductPicture GetDetails(long id); 
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
