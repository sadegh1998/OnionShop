using ShopManagement.ApplicationContract.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_ShopQuery.Contracts.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetProductDetails(string slug);
        List<ProductQueryModel> GetLatestProducts();
        List<ProductQueryModel> Search(string value);
        List<CartItem> CheckInventoryStatus(List<CartItem> cartItems);
    }
}
