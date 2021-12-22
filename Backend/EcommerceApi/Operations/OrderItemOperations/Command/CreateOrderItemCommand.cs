using System;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Entities;

namespace EcommerceApi.Operations.OrderItemOperations.Command
{
    public class CreateOrderItemCommand
    {
        public CreateOrderItemModel Model { get; set; }
        private readonly ApplicationDbContext _dbContext;
        
        public CreateOrderItemCommand(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var OrderItem = new OrderItem();
            OrderItem.Quantity = Model.Quantity;
            OrderItem.CreateDate = Model.CreateDate;
            OrderItem.ProductId = Model.ProductId; 
            OrderItem.OrderDetailId = Model.OrderDetailId;

            _dbContext.OrderItems.Add(OrderItem);
            _dbContext.SaveChanges();
            
        }

        public class CreateOrderItemModel
        {
            public int Quantity { get; set; }
            public DateTime CreateDate { get; set; }
            public int ProductId { get; set; }
            public int OrderDetailId { get; set; }
        }
    }
}