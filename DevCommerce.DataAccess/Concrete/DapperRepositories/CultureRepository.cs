using DevCommerce.DataAccess.Concrete.DapperRepositories.Abstract;
using DevCommerce.Entities.Concrete;

namespace DevCommerce.DataAccess.Concrete.DapperRepositories
{
    public class CultureRepository : BaseRepository<Culture>, ICultureRepository
    {
        public CultureRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}
