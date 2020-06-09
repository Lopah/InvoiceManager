using System.Collections.Generic;

namespace InvoiceManager.Core.Models
{
    public class InvoiceModel
    {
        public bool Paid { get; set; }

        public ICollection<ItemModel> Items { get; set; }

        public double TotalPrice => GetTotalPrice( );


        private double GetTotalPrice()
        {
            double _price = 0;

            foreach (var item in Items)
            {
                _price += item.Price;
            }

            return _price;
        }

    }
}
