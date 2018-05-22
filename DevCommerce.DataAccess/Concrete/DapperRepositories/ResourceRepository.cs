using DevCommerce.DataAccess.Concrete.DapperRepositories.Abstract;
using DevCommerce.Entities.Concrete;

namespace DevCommerce.DataAccess.Concrete.DapperRepositories
{
    public class ResourceRepository : BaseRepository<Resource>, IResourceRepository
    {
        public ResourceRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}
