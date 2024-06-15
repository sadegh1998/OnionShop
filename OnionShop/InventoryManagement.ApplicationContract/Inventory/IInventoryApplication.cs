using _0_Framework.Application;
using InventoryManagement.ApplicationContract.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace InventoryManagement.ApplicationContract.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Reduce(DecreaseInventory command);

        OperationResult Reduce(List<DecreaseInventory> command);
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        List<InventoryOperationsViewModel> GetOperationLog(long inventoryId);

    }
}
