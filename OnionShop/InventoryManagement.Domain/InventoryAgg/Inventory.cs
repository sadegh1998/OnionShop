using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public long ProductId { get; private set; }
        public int UnitPrice { get; private set; }
        public bool InStock { get; private set; }
        public List<InventoryOperation> Operations { get; private set; }

        public Inventory(long productId, int unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
        }

        public long CalculateCurrentInventoryStock()
        {
            var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
            var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - minus;
        }

        public void Increase(long count ,long operatorId , string description)
        {
            var curretncount = CalculateCurrentInventoryStock() + count;
            var operation = new InventoryOperation(true, count, operatorId, curretncount, description, 0, Id);
            Operations.Add(operation);
            InStock = curretncount > 0;
        }
        public void Reduce(long count, long operatorId, string description , long orderId)
        {
            var curretncount = CalculateCurrentInventoryStock() - count;
            var operation = new InventoryOperation(false, count, operatorId, curretncount, description, orderId, Id);
            Operations.Add(operation);
            InStock = curretncount > 0;
        }
    }
    

    public class InventoryOperation
    {
        public long Id { get; private set; }
        public bool Operation { get; private set; }
        public long Count { get; private set; }
        public long OperationId { get; private set; }
        public DateTime OperationDate { get; private set; }
        public long CurrentCount { get; private set; }
        public string Description { get; private set; }
        public long OrederId { get; private set; }
        public long InventoryId { get; private set; }
        public Inventory Inventory { get; private set; }

        public InventoryOperation(bool operation, long count, long operationId, long currentCount,
            string description, long orederId, long inventoryId)
        {
            Operation = operation;
            Count = count;
            OperationId = operationId;
            CurrentCount = currentCount;
            Description = description;
            OrederId = orederId;
            InventoryId = inventoryId;
            OperationDate = DateTime.Now;
        }
    }

}
