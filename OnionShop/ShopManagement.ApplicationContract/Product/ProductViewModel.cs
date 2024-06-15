namespace ShopManagement.ApplicationContract.Product
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Code { get; set; }
        public string CreationDate { get; set; }
        public long CategotyId{ get; set; }
        public string Category { get; set; }
    }
}
