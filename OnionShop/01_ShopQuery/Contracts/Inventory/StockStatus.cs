using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_ShopQuery.Contracts.Inventory
{
    public class StockStatus
    {
        public bool InStock { get; set; }
        public string ProductName { get; set; }
    }
}
