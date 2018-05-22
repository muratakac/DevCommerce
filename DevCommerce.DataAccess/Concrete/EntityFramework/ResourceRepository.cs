﻿using DevCommerce.DataAccess.Concrete.EntityFramework.Abstract;
using DevCommerce.Entities.Concrete;

namespace DevCommerce.DataAccess.Concrete.EntityFramework
{
    public class ResourceRepository : BaseRepository<Resource>, IResourceRepository
    {
        public ResourceRepository(DevCommerceContext context) : base(context)
        {
        }
    }
}
