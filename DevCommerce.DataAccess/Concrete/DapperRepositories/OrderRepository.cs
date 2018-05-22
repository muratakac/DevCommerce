using DevCommerce.DataAccess.Concrete.DapperRepositories.Abstract;
using DevCommerce.Entities;

namespace DevCommerce.DataAccess.Concrete.DapperRepositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}
