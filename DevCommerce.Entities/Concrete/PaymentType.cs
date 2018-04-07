using DevCommerce.Core.Entities;
using System.Collections.Generic;

namespace DevCommerce.Entities
{
    public partial class PaymentType : IEntity
    {
        public PaymentType()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }

        public int PaymentTypeId { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
