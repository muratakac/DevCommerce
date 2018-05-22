using DevCommerce.DataAccess.Concrete.EntityFramework.Abstract;
using DevCommerce.Entities.Concrete;

namespace DevCommerce.DataAccess.Concrete.EntityFramework
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(DevCommerceContext context) : base(context)
        {

        }
    }
}
