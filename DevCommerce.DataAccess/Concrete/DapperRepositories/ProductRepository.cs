using DevCommerce.DataAccess.Concrete.DapperRepositories.Abstract;
using DevCommerce.Entities;
using System;
using System.Linq;

namespace DevCommerce.DataAccess.Concrete.DapperRepositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public IQueryable<Product> AllWithChild()
        {
            throw new NotImplementedException();
        }
    }
}
