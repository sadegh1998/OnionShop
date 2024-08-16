using _0_Framework.Application;
using InventoryManagement.ApplicationContract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository, IAuthHelper authHelper)
        {
            _inventoryRepository = inventoryRepository;
            _authHelper = authHelper;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();
            if(_inventoryRepository.Exisit(x=>x.ProductId == command.ProductId))
            {
                return operation.Failed(ApplicationMessages.Duplicate);
            }

            var ineventory = new Inventory(command.ProductId,command.UnitPrice);
            _inventoryRepository.Create(ineventory);
            _inventoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.Id);
            if(inventory == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }

            if(_inventoryRepository.Exisit(x=>x.ProductId == command.ProductId && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.Duplicate);
            }

            inventory.Edit(command.ProductId, command.UnitPrice);
            _inventoryRepository.SaveChanges();
            return operation.Success();
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public List<InventoryOperationsViewModel> GetOperationLog(long inventoryId)
        {
            return _inventoryRepository.GetOperationLog(inventoryId);
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if(inventory == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }

            inventory.Increase(command.Count,1,command.Description);
            _inventoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Reduce(DecreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }

            inventory.Reduce(command.Count, 1, command.Description,0);
            _inventoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Reduce(List<DecreaseInventory> command)
        {
            var operation = new OperationResult();
            var operatorId = _authHelper.CurrentAccountId();
            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetBy(item.ProductId);
                inventory.Reduce(item.Count, operatorId, item.Description, item.OrderId);
            }
            _inventoryRepository.SaveChanges();
            return operation.Success();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }
    }
}
