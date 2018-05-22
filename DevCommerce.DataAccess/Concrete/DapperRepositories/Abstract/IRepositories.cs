using System.Collections.Generic;
using System.Linq;

namespace DevCommerce.DataAccess.Concrete.DapperRepositories.Abstract
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();
        T Find(IDictionary<string, string> keyValues);
        T Create(T entity);

    }
}
