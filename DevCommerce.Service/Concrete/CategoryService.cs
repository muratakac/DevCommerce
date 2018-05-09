using DevCommerce.Business.Abstract;
using DevCommerce.DataAccess.Abstract;
using DevCommerce.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevCommerce.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _categoryRepository;
        IDistributedCache _distributedCache;

        public CategoryService(ICategoryRepository categoryRepository, IDistributedCache distributedCache)
        {
            _categoryRepository = categoryRepository;
            _distributedCache = distributedCache;
        }

        //TODO => Aspect
        public List<Category> GetAll()
        {
            List<Category> categories = null;
            var cacheKey = "AllCategoryList";

            string jsonFormatOfCategories = _distributedCache.GetString(cacheKey);

            if (string.IsNullOrEmpty(jsonFormatOfCategories))
            {
                categories = _categoryRepository.All().ToList();
                //var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10));
                //option.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                _distributedCache.SetString(cacheKey, JsonConvert.SerializeObject(categories));
            }
            else
            {
                categories = JsonConvert.DeserializeObject<List<Category>>(jsonFormatOfCategories);
            }

            return categories;
        }

        public Category GetById(int id)
        {
            return _categoryRepository.Find(id);
        }

        public Category Insert(Category category)
        {
            return _categoryRepository.Create(category);
        }

        public int Update(Category category)
        {
            return _categoryRepository.Update(category);
        }
    }
}
