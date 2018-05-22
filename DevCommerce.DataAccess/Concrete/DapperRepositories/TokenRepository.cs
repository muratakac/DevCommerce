using DevCommerce.DataAccess.Concrete.DapperRepositories.Abstract;
using DevCommerce.Entities.Concrete;

namespace DevCommerce.DataAccess.Concrete.DapperRepositories
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}
