using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.ApplicationContract.ProductCategory
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get;  set; }
        public string Description { get;  set; }
        public IFormFile Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get; set; }
    }
}
