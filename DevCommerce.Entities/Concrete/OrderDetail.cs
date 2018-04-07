using System;

namespace DevCommerce.Entities
{
    public partial class OrderDetail 
    {
     
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string UnitPrice { get; set; }
        public Nullable<short> Discount { get; set; }
        public Nullable<short> Quantity { get; set; }
        public Nullable<int> PaymentTypeId { get; set; }
        public Nullable<int> InstallmentNumber { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual PaymentType PaymentType { get; set; }
    }
}
