﻿using DevCommerce.DataAccess.Concrete.EntityFramework.Abstract;
using DevCommerce.Entities;

namespace DevCommerce.DataAccess.Concrete.EntityFramework
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DevCommerceContext context) : base(context)
        {

        }
    }
}
