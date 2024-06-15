using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.ApplicationContract.Inventory
{
    public class InventoryOperationsViewModel
    {
        public long Id { get;   set; }
        public long Count { get; set; }

        public bool Operation { get;   set; }
        public long OperationId { get;   set; }
        public string Operator { get; set; }
        public string OperationDate { get;   set; }
        public long CurrentCount { get;   set; }
        public string Description { get;   set; }

        
    }
}
