using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Operations.ProductOperations.Query;
using EcommerceApi.Operations.ProductOperations.Command;
using EcommerceApi.Entities;
using static EcommerceApi.Operations.ProductOperations.Command.CreateProductCommand;

namespace EcommerceApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class ProductController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            GetProductsQuery query = new GetProductsQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public Product GetById(int id)
        {
            var product = _context.Products.Where(product => product.Id == id).SingleOrDefault();
            return product;
        }

        //Post
        [HttpPost]
        public IActionResult AddProduct([FromBody] CreateProductModel newProduct)
        {
            CreateProductCommand command = new CreateProductCommand(_context);

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
        public IActionResult UpdateBook(int id, [FromBody] Product updatedProduct)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == id);

            if (product == null)
                return BadRequest();

            product.CategoryId = updatedProduct.CategoryId != default ? updatedProduct.CategoryId : product.CategoryId;
            product.Title = updatedProduct.Title != default ? updatedProduct.Title : product.Title;
            product.Price = updatedProduct.Price != default ? updatedProduct.Price : product.Price;
            product.PictureUrl = updatedProduct.PictureUrl != default ? updatedProduct.PictureUrl : product.PictureUrl; 
            product.CategoryId = updatedProduct.CategoryId != default ? updatedProduct.CategoryId : product.CategoryId;
            _context.SaveChanges();
            return Ok();    
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
                return BadRequest();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok();
        }
    }
}