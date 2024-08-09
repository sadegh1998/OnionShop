using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public class OrderItem : EntityBase
    {
        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public int DiscountRate { get;private set; }
        public int Count { get; private set; }
        public long OrderId { get; private set; }
        public Order Order { get; private set; }

        public OrderItem(long productId, double unitPrice, int discountRate, int count)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            DiscountRate = discountRate;
            Count = count;
        }
    }
}
