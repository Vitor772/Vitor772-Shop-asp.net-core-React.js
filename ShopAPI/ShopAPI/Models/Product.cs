namespace ShopAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string CategoryProducts { get; set; }

        public string ProductName { get; set; }

        public float Price { get; set; }

        public string Photo { get; set; }

        public int Quantity { get; set; }
    }
}
