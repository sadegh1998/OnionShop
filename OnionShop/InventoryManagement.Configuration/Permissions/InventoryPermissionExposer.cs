using _0_Framework.Infrstructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Configuration.Permissions
{
    public class InventoryPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissonDto>> Expose()
        {
            return new Dictionary<string, List<PermissonDto>> 
            {
                {
                    "Inventory" , new List<PermissonDto>
                    { 
                        new PermissonDto(InventoryPermission.ListInventories,"ListInventories"),
                        new PermissonDto(InventoryPermission.SearchInventories,"SearchInventories"),
                        new PermissonDto(InventoryPermission.CreateInventory,"CreateInventory"),
                        new PermissonDto(InventoryPermission.EditInventory,"EditInventory"),
                        new PermissonDto(InventoryPermission.Increase,"Increase"),
                        new PermissonDto(InventoryPermission.Reduce,"Reduce"),
                        new PermissonDto(InventoryPermission.Operations,"Operations"),
                    }
                }
            };
        }
    }
}
