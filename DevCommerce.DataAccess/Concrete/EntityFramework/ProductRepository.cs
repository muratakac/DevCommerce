using DevCommerce.DataAccess.Abstract;
using DevCommerce.Entities;

namespace DevCommerce.DataAccess.Concrete.EntityFramework
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DevCommerceContext context) : base(context)
        {

        }
    }
}
