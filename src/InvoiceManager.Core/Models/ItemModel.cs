﻿namespace InvoiceManager.Core.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public int InvoiceId { get; set; }
    }
}
