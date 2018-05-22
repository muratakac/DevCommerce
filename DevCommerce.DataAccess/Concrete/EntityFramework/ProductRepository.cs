using DevCommerce.DataAccess.Concrete.EntityFramework.Abstract;
using DevCommerce.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DevCommerce.DataAccess.Concrete.EntityFramework
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DevCommerceContext context) : base(context)
        {

        }

        public virtual IQueryable<Product> AllWithChild()
        {
            return Context.Products.Include(x => x.Images).ThenInclude(y => y.Image);
        }
    }
}
