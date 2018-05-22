using DevCommerce.DataAccess.Concrete.DapperRepositories.Abstract;
using DevCommerce.Entities;

namespace DevCommerce.DataAccess.Concrete.DapperRepositories
{
    public class CategoryRepository : BaseRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}
