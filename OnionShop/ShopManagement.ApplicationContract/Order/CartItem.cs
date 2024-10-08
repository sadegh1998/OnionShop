﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.ApplicationContract.Order
{
    public class CartItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public double UnitPrice { get; set; }
        public double TotalItemPrice { get; set; }
        public int DiscountRate { get; set; }
        public double DiscountItemAmount { get; set; }
        public double PayItemAmount { get; set; }
        public int Count { get; set; }
        public bool IsInStock { get; set; }

        public void CalculateTotalItemAmount()
        {
            TotalItemPrice = UnitPrice * Count;
        }
    }
}
