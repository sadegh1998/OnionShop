using InventoryManagement.ApplicationContract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.ApplicationContract.Product;

namespace ServiceHost.Areas.Administrator.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public InventorySearchModel SearchModel { get; set; }
        public List<InventoryViewModel> Inevtories{ get; set; }
        public SelectList Products { get; set; }
        private readonly IProductApplication _product;
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel(IInventoryApplication inventoryApplication, IProductApplication product)
        {
            _inventoryApplication = inventoryApplication;
            _product = product;
        }

        public void OnGet(InventorySearchModel searchModel)
        {
            Products = new SelectList(_product.GetProducts(), "Id", "Name");
            Inevtories = _inventoryApplication.Search(searchModel);
        }
        
        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory();
            command.Products = _product.GetProducts();
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate( CreateInventory command)
        {
           
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetEdit(long id)
        {
            var inventory = _inventoryApplication.GetDetails(id);
            inventory.Products = _product.GetProducts();
            return Partial("./Edit", inventory);
        }
        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);

        }
       

    }
}
