using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Configuration.Permissions
{
    public static class InventoryPermission
    {
        public const int ListInventories = 50;
        public const int SearchInventories = 51;
        public const int CreateInventory = 52;
        public const int EditInventory = 53;
        public const int Increase = 54;
        public const int Reduce = 55;
        public const int Operations = 56;
    }
}
