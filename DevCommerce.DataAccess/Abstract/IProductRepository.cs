using DevCommerce.Entities;
using System.Linq;

namespace DevCommerce.DataAccess.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> AllWithChild();
    }
}
