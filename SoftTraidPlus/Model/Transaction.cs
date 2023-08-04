﻿using Microsoft.SqlServer.Server;
using System;

namespace SoftTradePlus.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public int ClientId { get; set; } // кто купил
        public int ProductId { get; set; } // id купленного товара
        public bool IsUnlimited { get; set; } // постоянная ли лицензия
        public DateTime PurchaseDate { get; set; } // дата покупки
        public DateTime ExpiryDate { get; set; } // дата истечение срока подписки
        
    }
}
