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
                        new PermissonDto(50,"ListInventories"),
                        new PermissonDto(51,"SearchInventories"),
                        new PermissonDto(53,"CreateInventory"),
                        new PermissonDto(54,"EditInventory"),



                    }
                }
            };
        }
    }
}
