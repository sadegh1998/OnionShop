using _0_Framework.Application;
using ShopManagement.ApplicationContract.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProiductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;
        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IFileUploader fileUploader, IProductRepository productRepository)
        {
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            //if(_productPictureRepository.Exisit(x=>x.Picture == command.Picture && x.ProductId == command.ProductId))
            //{
            //    return operation.Failed(ApplicationMessages.Duplicate);
            //}


            var product = _productRepository.GetProductWithCategoryBy(command.ProductId);
            var path = $"{product.Category.Slug}/{product.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);
            var productPicture = new ProductPicture(command.ProductId, picturePath, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetProductAndCategoryBy(command.Id);
            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }
            //if (_productPictureRepository.Exisit(x => x.Picture == command.Picture && x.ProductId != command.ProductId))
            //{
            //    return operation.Failed(ApplicationMessages.Duplicate);
            //}
            var path = $"{productPicture.Product.Category.Slug}/{productPicture.Product.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            productPicture.Edit(command.ProductId, picturePath, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.SaveChanges();
            return operation.Success();
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);

            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }

            productPicture.Remove();
            _productPictureRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);

            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }

            productPicture.Restore();
            _productPictureRepository.SaveChanges();
            return operation.Success();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}
