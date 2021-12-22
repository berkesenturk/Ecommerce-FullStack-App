using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Operations.CategoryOperations.Query;
using EcommerceApi.Operations.CategoryOperations.Command;
using EcommerceApi.Entities;
using static EcommerceApi.Operations.CategoryOperations.Command.CreateCategoryCommand;

namespace EcommerceApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class CategoryController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            GetCategoriesQuery query = new GetCategoriesQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public Category GetById(int id)
        {
            var category = _context.Categories.Where(category => category.Id == id).SingleOrDefault();
            return category;
        }

        //Post
        [HttpPost]
        public IActionResult AddProduct([FromBody] CreateCategoryModel newProduct)
        {
            CreateCategoryCommand command = new CreateCategoryCommand(_context);

            try
            {
                command.Model = newProduct;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        //Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Category updatedCategory)
        {
            var category = _context.Categories.SingleOrDefault(x => x.Id == id);

            if (category == null)
                return BadRequest();

            category.Id = updatedCategory.Id != default ? updatedCategory.Id : category.Id;
            category.Name = updatedCategory.Name != default ? updatedCategory.Name : category.Name;
            
            _context.SaveChanges();
            return Ok();    
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var category = _context.Categories.SingleOrDefault(x => x.Id == id);
            if (category == null)
                return BadRequest();

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Ok();
        }
    }
}