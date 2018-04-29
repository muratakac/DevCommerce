using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevCommerce.Entities
{
    public abstract class BasketProduct
    {
        public virtual int ProductId { get; set; }
        public virtual decimal UnitPrice { get; set; }

        [NotMapped]
        public int Quantity { get; set; }

        [NotMapped]
        public decimal SubTotal
        {
            get
            {
                return Quantity * UnitPrice;
            }
        }
    }
}
