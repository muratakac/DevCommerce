using DevCommerce.DataAccess.Abstract;
using DevCommerce.Entities;

namespace DevCommerce.DataAccess.Concrete.EntityFramework
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(DevCommerceContext context) : base(context)
        {

        }
    }
}
