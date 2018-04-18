using DevCommerce.DataAccess.Abstract;
using DevCommerce.Entities.Concrete;

namespace DevCommerce.DataAccess.Concrete.EntityFramework
{
    public class CultureRepository : BaseRepository<Culture>, ICultureRepository
    {
        public CultureRepository(DevCommerceContext context) : base(context)
        {
        }
    }
}
