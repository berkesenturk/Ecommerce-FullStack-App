using System;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApi.Operations.ProductOperations.Command
{
    public class UpdateProductCommand
    {
        public UpdateProductModel Model { get; set; }
        private readonly ApplicationDbContext _dbContext;
        
        public UpdateProductCommand(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var product = _dbContext.Products.SingleOrDefault(x => x.Title == Model.Title);

            if(product is not null)
                throw new InvalidOperationException("product exists already.");

            product = new Product();
            product.Title = Model.Title;
            product.Price = Model.Price;
            product.PictureUrl = Model.PictureUrl;
            product.CategoryId = Model.CategoryId;
            product.VendorId = Model.VendorId;
            
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }
    }
        public class UpdateProductModel
    {
        public string Title { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int CategoryId { get; set; }
        public int VendorId { get; set; }

    }
}
