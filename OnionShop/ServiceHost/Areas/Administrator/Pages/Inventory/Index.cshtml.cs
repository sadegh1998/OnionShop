using _0_Framework.Infrstructure;
using InventoryManagement.ApplicationContract.Inventory;
using InventoryManagement.Configuration.Permissions;
using InventoryManagement.Domain.InventoryAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.ApplicationContract.Product;
using ShopManagement.Configuration.Permissions;

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
        [NeedsPermission(InventoryPermission.CreateInventory)]
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
        [NeedsPermission(InventoryPermission.EditInventory)]
        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetIncrease(long id)
        {
            var inventory = new IncreaseInventory { 
            InventoryId = id
            };
            return Partial("./Increase", inventory);
        }
        [NeedsPermission(InventoryPermission.Increase)]
        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            var result = _inventoryApplication.Increase(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetReduce(long id)
        {
            var inventory = new DecreaseInventory
            {
                InventoryId = id
            };
            return Partial("./Reduce", inventory);
        }
        [NeedsPermission(InventoryPermission.Reduce)]
        public JsonResult OnPostReduce(DecreaseInventory command)
        {
            var result = _inventoryApplication.Reduce(command);
            return new JsonResult(result);

        }
        [NeedsPermission(InventoryPermission.Operations)]
        public IActionResult OnGetOperationLogs(long id)
        {
            var logs = _inventoryApplication.GetOperationLog(id);
            return Partial("./OperationLogs", logs);

        }


    }
}
