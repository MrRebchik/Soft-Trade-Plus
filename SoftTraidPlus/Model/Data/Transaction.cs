using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

public enum Duration
{
    Month,
    Quarter,
    Year
}

namespace SoftTradePlus.Model.Data
{
    public class Transaction
    {
        public int Id { get; set; }

        [NotMapped]
        public string Name
        {
            get
            {
                if (SQLHelper.GetAllProducts().Count() > 0)
                    return SQLHelper.GetAllProducts().Where(el => el.Id == ProductId).First().Name;
                else return "";
            }
        }
        public int ClientId { get; set; } // кто купил
        public int ProductId { get; set; } // id купленного товара
        public bool IsUnlimited { get; set; } // постоянная ли лицензия
        public DateTime? PurchaseDate { get; set; } // дата покупки
        public DateTime? ExpiryDate { get; set; } // дата истечение срока подписки

    }
}
