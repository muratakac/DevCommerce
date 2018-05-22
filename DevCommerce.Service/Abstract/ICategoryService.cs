using DevCommerce.Entities;
using System.Collections.Generic;

namespace DevCommerce.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(int id);
        Category Insert(Category category);
        //int Update(Category category);
    }
}
