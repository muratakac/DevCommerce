using DevCommerce.Core.Entities;
using System;
using System.Collections.Generic;

namespace DevCommerce.Entities
{
    public partial class Order : IEntity
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> ShipAddressId { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> RequiredDate { get; set; }
        public Nullable<int> CargoCompanyId { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
