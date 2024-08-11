using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order : EntityBase
    {
        public long AccountId { get;private set; }
        public double TotalPrice { get; private set; }
        public double TotalDiscount { get; private set; }
        public double PayAmount { get; private set; }
        public int PaymentMethod { get; private set; }

        public bool IsPaid { get; private set; }
        public bool IsCanceled { get; private set; }
        public string IssueTrackingNo { get; private set; }
        public long RefId { get; private set; }
        public List<OrderItem> Items { get; private set; }

        public Order(long accountId, double totalPrice, double totalDiscount, double payAmount, int paymentMethod)
        {
            AccountId = accountId;
            TotalPrice = totalPrice;
            TotalDiscount = totalDiscount;
            PayAmount = payAmount;
            PaymentMethod = paymentMethod;
            RefId = 0;
            IsPaid = false;
            IsCanceled = false;
            Items = new List<OrderItem>();
        }
        public void Cancel()
        {
            IsCanceled = true;
        }
        public void PaymentSucceded(long refId)
        {
            IsPaid = true;
            if(RefId == 0)
            {
                RefId = refId;
            }
        }
        public void SetIssueTrackingNo(string number)
        {
            IssueTrackingNo = number;
        }
        public void AddItem(OrderItem orderItem)
        {
            Items.Add(orderItem);
        }

    }
}
