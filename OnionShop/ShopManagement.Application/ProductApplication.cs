using _0_Framework.Application;
using ShopManagement.ApplicationContract.Product;
using ShopManagement.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if(_productRepository.Exisit(x=>x.Name == command.Name))
            {
                return operation.Failed(ApplicationMessages.Duplicate);
            }
            var slug = command.Slug.Slugify();
            var product = new Product(command.Name, command.Code, command.UnitPrice,command.Description, command.ShortDescription, command.Picture, command.PictureAlt, command.PictureTitle, slug, command.Keywords, command.MetaDescription, command.CategoryId);
            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            if(_productRepository.Exisit(x=>x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.Duplicate);
            }
            var product = _productRepository.Get(command.Id);
            if (product == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }

            var slug = command.Slug.Slugify();
            product.Edit(command.Name, command.Code, command.UnitPrice,command.Description, command.ShortDescription, command.Picture, command.PictureAlt, command.PictureTitle, slug, command.Keywords, command.MetaDescription, command.CategoryId);
            _productRepository.SaveChanges();
            return operation.Success();


        }

        public EditProduct Get(long id)
        {
            return _productRepository.GetDetails(id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public OperationResult InStock(long id)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(id);
            if (product == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }
            product.InStock();
            _productRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult NotInStock(long id)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(id);
            if(product == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }
            product.IsNotInStock();
            _productRepository.SaveChanges();
            return operation.Success();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }
    }
}
