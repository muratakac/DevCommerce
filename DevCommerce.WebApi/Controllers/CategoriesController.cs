﻿using DevCommerce.Business.Abstract;
using DevCommerce.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DevCommerce.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Categories")]
    public class CategoriesController : Controller
    {
        private ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [Authorize()]
        [HttpGet]
        public List<Category> GetCategories()
        {
            return _categoryService.GetAll();
        }

        [Authorize()]
        [HttpGet("{categoryId}")]
        public Category GetCategoryByCategoryId(int categoryId)
        {
            return _categoryService.GetById(categoryId);
        }
    }
}