using DevCommerce.Business.Abstract;
using DevCommerce.Core.CrossCuttingConcerns.Cache;
using DevCommerce.Core.CrossCuttingConcerns.Cache.Redis;
using DevCommerce.DataAccess.Concrete.DapperRepositories.Abstract;
using DevCommerce.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DevCommerce.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //[CacheProvider(ProviderType = typeof(RedisCacheProvider), Duration = 10)]
        public List<Category> GetAll()
        {
            return _categoryRepository.All().ToList();
        }

        //Dapper
        public Category GetById(int id)
        {
            return _categoryRepository.Find(new Dictionary<string, string>() { { "CategoryId", id.ToString() } });
        }

        //EF
        //public Category GetById(int id)
        //{
        //    return _categoryRepository.Find(id);
        //}

        public Category Insert(Category category)
        {
            return _categoryRepository.Create(category);
        }

        //public int Update(Category category)
        //{
        //    return _categoryRepository.Update(category);
        //}
    }
}
