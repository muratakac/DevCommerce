using DevCommerce.DataAccess.Concrete.DapperRepositories.Abstract;
using DevCommerce.Entities;

namespace DevCommerce.DataAccess.Concrete.DapperRepositories
{
    public class BrandRepository : BaseRepository<Brand>,IBrandRepository
    {
        public BrandRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}
