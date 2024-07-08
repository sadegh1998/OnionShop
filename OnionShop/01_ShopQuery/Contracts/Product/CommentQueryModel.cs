namespace _01_ShopQuery.Contracts.Product
{
    public class CommentQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public long ProductId { get; set; }
    }
}
