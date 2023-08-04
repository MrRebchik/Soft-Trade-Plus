namespace SoftTradePlus.Model
{
    public enum ProductType
    {
        Unlimited = 0,
        Limited = 1
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public ProductType Type { get; set; }
    }
}
