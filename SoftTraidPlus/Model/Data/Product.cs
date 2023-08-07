using System.ComponentModel.DataAnnotations.Schema;

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
        public double Price { get; set; }

        [NotMapped]
        public string TypeName
        {
            get
            {
                if (this.Type == ProductType.Limited)
                {
                    return "Продливаемый";
                }
                else
                {
                    return "Бессрочный";
                }
            }
        }
        public ProductType Type { get; set; }
    }
}
