using DevCommerce.Business.Abstract;
using DevCommerce.Core.CrossCuttingConcerns.Cache;
using DevCommerce.Core.CrossCuttingConcerns.Cache.Redis;
using DevCommerce.DataAccess.Concrete.DapperRepositories.Abstract;
using DevCommerce.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DevCommerce.Business.Concrete
{
    public class BrandService : IBrandService
    {
        IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        //[CacheProvider(ProviderType = typeof(RedisCacheProvider), Duration = 10)]
        public List<Brand> GetAll()
        {
            return _brandRepository.All().ToList();
        }

        //Dapper
        public Brand GetById(int id)
        {
            return _brandRepository.Find(new Dictionary<string, string>() { { "BrandId", id.ToString() } });
        }

        //public Brand Insert(Brand brand)
        //{
        //    return _brandRepository.Create(brand);
        //}

        //public int Update(Brand brand)
        //{
        //    return _brandRepository.Update(brand);
        //}
    }
}
