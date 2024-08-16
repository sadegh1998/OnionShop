namespace ShopManagement.ApplicationContract.Order
{
    public class OrderviewModel
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string AccountFullName { get; set; }
        public double TotalPrice { get; set; }
        public double TotalDiscount { get; set; }
        public double PayAmount { get; set; }
        public int PaymentMethodId { get; set; }

        public string PaymentMethod { get; set; }

        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }
        public string IssueTrackingNo { get; set; }
        public long RefId { get; set; }
        public string CreationDate { get; set; }
    }
}
