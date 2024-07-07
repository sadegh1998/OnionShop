using Microsoft.AspNetCore.Http;
using ShopManagement.ApplicationContract.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.ApplicationContract.ProductPicture
{
    public class CreateProductPicture
    {
        public long ProductId { get;  set; }
        public IFormFile Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
