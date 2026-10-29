namespace KassasystemProject
{
    public class CartItem
    {
        public Product Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalPrice => Product.Price * Quantity;
        public CartItem(Product product, decimal quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}







      

       