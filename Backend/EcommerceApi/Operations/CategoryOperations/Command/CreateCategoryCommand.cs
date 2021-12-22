using System;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Entities;

namespace EcommerceApi.Operations.CategoryOperations.Command
{
    public class CreateCategoryCommand
    {
        public CreateCategoryModel Model { get; set; }
        private readonly ApplicationDbContext _dbContext;
        
        public CreateCategoryCommand(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var category = _dbContext.Categories.SingleOrDefault(x => x.Name == Model.Name);

            if(category is not null)
                throw new InvalidOperationException("Category exists already.");

            category = new Category();
            category.Name = Model.Name;
            
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            
        }


    }
    public class CreateCategoryModel
    {
        public string Name { get; set; }
    }
}