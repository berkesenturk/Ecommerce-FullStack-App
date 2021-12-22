using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Entities;

namespace EcommerceApi.Operations.CategoryOperations.Query
{
        public class GetCategoriesQuery
        {
            private readonly ApplicationDbContext _dbContext;

            public GetCategoriesQuery(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public List<CategoriesViewModel> Handle()
            {
                var categoryList = _dbContext.Categories.OrderBy(x => x.Id).ToList<Category>(); 
                List<CategoriesViewModel> vm = new List<CategoriesViewModel>();


                foreach (var category in categoryList)
                {
                    vm.Add(new CategoriesViewModel()
                    {
                        Id = category.Id,
                        Name = category.Name
                    });
                }
                return vm;
        }       

        public class CategoriesViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            
        }
    }
}