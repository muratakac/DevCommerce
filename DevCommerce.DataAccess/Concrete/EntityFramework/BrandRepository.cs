using DevCommerce.DataAccess.Concrete.EntityFramework.Abstract;
using DevCommerce.Entities;

namespace DevCommerce.DataAccess.Concrete.EntityFramework
{
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        public BrandRepository(DevCommerceContext context) : base(context)
        {

        }
    }
}
