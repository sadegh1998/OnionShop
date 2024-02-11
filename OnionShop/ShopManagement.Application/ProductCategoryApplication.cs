using _0_Framework.Application;
using ShopManagement.ApplicationContract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _categoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_categoryRepository.Exisit(x => x.Name == command.Name))
                return operation.Failed("رکوردی دیگری با همین عنوان یافت شد . نام دیگری وارد کنید");

            var slug = command.Slug.Slugify();
            var productCategory = new ProductCategory(command.Name, command.Description, command.Picture, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            _categoryRepository.Create(productCategory);
            _categoryRepository.SaveChanges();

           return operation.Success();

        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            if (_categoryRepository.Exisit(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed("رکوردی دیگری با همین عنوان یافت شد . نام دیگری وارد کنید");

            var productCategory = _categoryRepository.Get(command.Id);
            if (productCategory == null)
                return operation.Failed("رکوردی با مشخصات وارد شده پیدا نشد . مججددا تلاش نمایید");

            var slug = command.Slug.Slugify();
            productCategory.Edit(command.Name, command.Description, command.Picture, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            _categoryRepository.SaveChanges();

            return operation.Success();


        }

        public EditProductCategory Get(long Id)
        {
            var productCategory = _categoryRepository.Get(Id);
            return new EditProductCategory
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description,
                Keywords = productCategory.Keywords,
                MetaDescription = productCategory.MetaDescription,
                Picture = productCategory.Picture,
                PictureAlt = productCategory.PictureAlt,
                PictureTitle = productCategory.PictureTitle,
                Slug = productCategory.Slug
            };
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var productCategory = _categoryRepository.Search(searchModel.Name);
            return productCategory.Select(x => new ProductCategoryViewModel { 
                Id  = x.Id,
                Name = x.Name , 
                CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture) ,
                Picture = x.Picture ,
                ProductCounts =0
            }).ToList();
        }
    }
}
