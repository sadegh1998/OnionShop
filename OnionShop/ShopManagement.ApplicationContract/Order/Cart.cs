namespace ShopManagement.ApplicationContract.Order
{
    public class Cart
    {
        public double Amount { get; set; }
        public double DiscountAmount { get; set; }
        public double PayAmount { get; set; }
        public List<CartItem> Items { get; set; }

        public Cart()
        {
            Items = new List<CartItem>();
        }
        public void Add(CartItem cartItems)
        {
            Items.Add(cartItems);
            Amount += cartItems.TotalItemPrice;
            DiscountAmount += cartItems.DiscountItemAmount;
            PayAmount += cartItems.PayItemAmount;

        }
    }
    
}
