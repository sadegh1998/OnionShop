using _0_Framework.Application;
using ShopManagement.ApplicationContract.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductApplication(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if(_productRepository.Exisit(x=>x.Name == command.Name))
            {
                return operation.Failed(ApplicationMessages.Duplicate);
            }
            var slug = command.Slug.Slugify();
            var productSlug = _productCategoryRepository.GetSlugby(command.CategoryId);
            var path = $"{productSlug}/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture , path);
            var product = new Product(command.Name, command.Code,command.Description, command.ShortDescription, picturePath, command.PictureAlt, command.PictureTitle, slug, command.Keywords, command.MetaDescription, command.CategoryId);
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
            var product = _productRepository.GetProductWithCategoryBy(command.Id);
            if (product == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }

            var slug = command.Slug.Slugify();
            var path = $"{product.Category.Slug}/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            product.Edit(command.Name, command.Code,command.Description, command.ShortDescription, picturePath, command.PictureAlt, command.PictureTitle, slug, command.Keywords, command.MetaDescription, command.CategoryId);
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



        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }
    }
}
