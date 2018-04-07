using DevCommerce.Business.Abstract;
using DevCommerce.DataAccess.Abstract;
using DevCommerce.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DevCommerce.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.All().ToList();
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
