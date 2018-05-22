using DevCommerce.Entities;
using System.Linq;

namespace DevCommerce.DataAccess.Concrete.DapperRepositories.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> AllWithChild();
    }
}
