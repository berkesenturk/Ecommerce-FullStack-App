using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApi.Operations.OrderItemOperations.Query
{
        public class GetOrderItemsQuery
        {
            private readonly ApplicationDbContext _dbContext;

            public GetOrderItemsQuery(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public List<OrderItemsViewModel> Handle()
            {
                var OrderItemList = _dbContext.OrderItems.OrderBy(x => x.Id).ToList<OrderItem>();  
                List<OrderItemsViewModel> vm = new List<OrderItemsViewModel>();   

                var orderDetailList = _dbContext.OrderDetails.OrderBy(x => x.Id).ToList<OrderDetail>();
                List<OrderDetailsViewModel> orderDetailVm = new List<OrderDetailsViewModel>();

                var productsList = _dbContext.Products.OrderBy(x => x.Id).ToList<Product>();
                List<ProductsViewModel> productVm = new List<ProductsViewModel>();

                foreach (var od in orderDetailList)
                {
                    orderDetailVm.Add(new OrderDetailsViewModel()
                    {
                        Id = od.Id,
                        TotalPrice = od.TotalPrice,
                        CreateDate = od.CreateDate
                    });
                }

                foreach (var OrderItem in OrderItemList)
                {
                    vm.Add(new OrderItemsViewModel()
                    {
                        Quantity = OrderItem.Quantity,
                        CreateDate = OrderItem.CreateDate,
                        OrderDetails = orderDetailVm.Where(c => c.Id == OrderItem.OrderDetailId).FirstOrDefault(),
                        Products = productVm.Where(c => c.Id == OrderItem.OrderDetailId).FirstOrDefault(),
                    });
                }
                return vm;
            }
        }

        public class OrderItemsViewModel
        {
            public int Quantity { get; set; }
            public DateTime CreateDate { get; set; }
            public ProductsViewModel Products { get; set; }
            public OrderDetailsViewModel OrderDetails { get; set; }
        }
        public class OrderDetailsViewModel
        {
            public int Id { get; set; }
            [Column(TypeName = "decimal(18,2)")]
            public decimal TotalPrice { get; set; }
            public DateTime CreateDate { get; set; }
        
        }
        public class ProductsViewModel
        {
            public int Id { get; set; }
            [Column(TypeName = "decimal(18,2)")]
            public string Title { get; set; }
            public decimal Price { get; set; }
            public string PictureUrl { get; set; }

        
        }
}