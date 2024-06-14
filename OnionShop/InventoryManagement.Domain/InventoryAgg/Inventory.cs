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

}
