using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApi.Operations.ProductOperations.Query
{
        public class GetProductsQuery
        {
            private readonly ApplicationDbContext _dbContext;

            public GetProductsQuery(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public List<ProductsViewModel> Handle()
            {
                var vendorList = _dbContext.Vendors.OrderBy(x => x.Id).ToList<Vendor>();
                var categoryList = _dbContext.Categories.OrderBy(x => x.Id).ToList<Category>();
                var productList = _dbContext.Products.OrderBy(x => x.Id).ToList<Product>();  

                List<VendorsViewModel> vendorVm = new List<VendorsViewModel>();
                List<ProductsViewModel> vm = new List<ProductsViewModel>();   
                List<CategoriesViewModel> categoryVm = new List<CategoriesViewModel>();

                foreach (var vendor in vendorList)
                {
                    vendorVm.Add(new VendorsViewModel()
                    {
                        Id = vendor.Id,
                        VendorName = vendor.VendorName,
                        FollowerCount = vendor.FollowerCount,
                        Rating = vendor.Rating
                    });
                }

                foreach (var category in categoryList)
                {
                    categoryVm.Add(new CategoriesViewModel()
                    {
                        Id = category.Id,
                        Name = category.Name
                    });
                }

                foreach (var product in productList)
                {
                    vm.Add(new ProductsViewModel()
                    {
                        Id = product.Id,
                        Title = product.Title,
                        Price = product.Price,
                        PictureUrl = product.PictureUrl,
                        Category = categoryVm.Where(c => c.Id == product.CategoryId).FirstOrDefault(),
                        Vendor = vendorVm.Where(v => v.Id == product.VendorId).FirstOrDefault()
                    });
                }
                return vm;
            }
        }

        public class ProductsViewModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            [Column(TypeName = "decimal(18,2)")]
            public decimal Price { get; set; }
            public string PictureUrl { get; set; }
            public CategoriesViewModel Category { get; set; }
            public VendorsViewModel Vendor { get; set; }
        }
        public class CategoriesViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            
        }
        public class VendorsViewModel
        {
            public int Id { get; set; }
            public string VendorName { get; set; }
            public int FollowerCount { get; set; }
            public double Rating { get; set; }
        }
}