using DevCommerce.Business.Abstract;
using DevCommerce.Core.CrossCuttingConcerns.Cache;
using DevCommerce.Core.CrossCuttingConcerns.Cache.Redis;
using DevCommerce.DataAccess.Abstract;
using DevCommerce.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DevCommerce.Business.Concrete
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [CacheProvider(ProviderType = typeof(RedisCacheProvider), Duration = 10)]
        public List<Product> GetAll()
        {
            return _productRepository.AllWithChild().ToList();
        }

        public Product GetById(int id)
        {
            return _productRepository.Find(id);
        }

        public Product Insert(Product product)
        {
            return _productRepository.Create(product);
        }

        public int Update(Product product)
        {
            return _productRepository.Update(product);
        }
    }
}
