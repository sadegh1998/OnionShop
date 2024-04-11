using _0_Framework.Application;
using ShopManagement.ApplicationContract.ProductPicture;
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

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();  
            if(_productPictureRepository.Exisit(x=>x.Picture == command.Picture && x.ProductId == command.ProductId))
            {
                return operation.Failed(ApplicationMessages.Duplicate);
            }
            var productPicture = new ProductPicture(command.ProductId , command.Picture,command.PictureAlt,command.PictureTitle);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(command.Id);
            if(productPicture == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }
            if(_productPictureRepository.Exisit(x=>x.Picture == command.Picture && x.ProductId != command.ProductId))
            {
                return operation.Failed(ApplicationMessages.Duplicate);
            }

            productPicture.Edit(command.ProductId, command.Picture, command.PictureAlt, command.PictureTitle);
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

            if(productPicture == null)
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
