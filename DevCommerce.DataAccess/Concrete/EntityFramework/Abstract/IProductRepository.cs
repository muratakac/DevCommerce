using DevCommerce.Entities;
using System.Linq;

namespace DevCommerce.DataAccess.Concrete.EntityFramework.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> AllWithChild();
    }
}
