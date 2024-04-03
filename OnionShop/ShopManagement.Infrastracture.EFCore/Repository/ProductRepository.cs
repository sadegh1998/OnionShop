using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;
using ShopManagement.ApplicationContract.Product;
using ShopManagement.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastracture.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<long , Product> , IProductRepository
    {
        private readonly ShopContext _shopContext;

        public ProductRepository(ShopContext shopContext) : base(shopContext) 
        {
            _shopContext = shopContext;
        }

        public EditProduct GetDetails(long id)
        {
            return _shopContext.Products.Select(x => new EditProduct { 
            Id = x.Id ,
            Name = x.Name ,
            Code = x.Code ,
            Description = x.Description,
            ShortDescription = x.ShortDescription,
            Slug = x.Slug ,
            CategoryId = x.CategoryId,
            IsInStock = x.IsInStock,
            Keywords = x.Keywords,
            MetaDescription = x.MetaDescription,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle ,
            UnitPrice = x.UnitPrice
            }).FirstOrDefault(x => x.Id == id);

        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var product = _shopContext.Products.Include(x=>x.Category).Select(x => new ProductViewModel {
            Id = x.Id ,
            Name = x.Name ,
            Code = x.Code ,
            Picture = x.Picture ,
            UnitPrice = x.UnitPrice ,
            CreationDate = x.CreationDate.ToString() , 
            InStock = x.IsInStock,
            CategotyId = x.Category.Id,
            Category = x.Category.Name
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                product = product.Where(x => x.Name.Contains(searchModel.Name));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
            {
                product = product.Where(x => x.Code.Contains(searchModel.Code));
            }

            if(searchModel.CategoryId != 0)
            {
                product = product.Where(x => x.CategotyId == searchModel.CategoryId);
            }

            return product.ToList();
        }
    }
}
