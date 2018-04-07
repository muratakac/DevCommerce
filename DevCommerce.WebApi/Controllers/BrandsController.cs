using DevCommerce.Business.Abstract;
using DevCommerce.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DevCommerce.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Brands")]
    public class BrandsController : Controller
    {
        private IBrandService _brandService;
        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public List<Brand> GetBrands()
        {
            return _brandService.GetAll();
        }

        [HttpGet("{brandId}")]
        public Brand GetBrandByBrandId(int brandId)
        {
            return _brandService.GetById(brandId);
        }

        //[HttpGet()]
        //public List<Brand> AllBrands()
        //{
        //    return _brandService.GetAll();
        //}

        //[HttpGet("{AllBrands2}")]
        //public List<Brand> AllBrands2()
        //{
        //    return _brandService.GetAll();
        //}
    }
}