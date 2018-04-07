using DevCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevCommerce.Business.Abstract
{
    public interface IBrandService
    {
        List<Brand> GetAll();
        Brand GetById(int id);
        Brand Insert(Brand category);
        int Update(Brand category);
    }
}
