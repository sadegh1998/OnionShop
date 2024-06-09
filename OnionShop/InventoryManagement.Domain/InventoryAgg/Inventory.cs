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
    }

    public class InventoryOperation
    {
        public long Id { get; private set; }
        public bool Operation { get; private set; }
        public int Count { get; private set; }
        public long OperationId { get; private set; }
        public DateTime OperationDate { get; private set; }
        public int CurrentCount { get; private set; }
        public string Description { get; private set; }
        public int OrederId { get; private set; }
        public long InventoryId { get; private set; }
        public Inventory Inventory { get; private set; }
    }
}
