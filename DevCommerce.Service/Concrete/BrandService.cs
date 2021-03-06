﻿using DevCommerce.Business.Abstract;
using DevCommerce.DataAccess.Abstract;
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

        public List<Brand> GetAll()
        {
            return _brandRepository.All().ToList();
        }

        public Brand GetById(int id)
        {
            return _brandRepository.Find(id);
        }

        public Brand Insert(Brand brand)
        {
            return _brandRepository.Create(brand);
        }

        public int Update(Brand brand)
        {
            return _brandRepository.Update(brand);
        }
    }
}
