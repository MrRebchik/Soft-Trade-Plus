namespace SoftTradePlus.Model.Data
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
        public float Price { get; set; }
        public ProductType Type { get; set; }
    }
}
