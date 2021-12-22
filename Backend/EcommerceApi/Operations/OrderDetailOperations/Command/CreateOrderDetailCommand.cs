using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Entities;

namespace EcommerceApi.Operations.OrderDetailOperations.Command
{
    public class CreateOrderDetailCommand
    {
        public CreateOrderDetailModel Model { get; set; }
        private readonly ApplicationDbContext _dbContext;
        
        public CreateOrderDetailCommand(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            
            var OrderDetail = new OrderDetail();
            OrderDetail.TotalPrice = Model.TotalPrice;
            OrderDetail.CreateDate = Model.CreateDate;
            OrderDetail.ClientId = Model.ClientId;
            _dbContext.OrderDetails.Add(OrderDetail);
            _dbContext.SaveChanges();
            
        }

        public class CreateOrderDetailModel
        {
            [Column(TypeName = "decimal(18,2)")]
            public decimal TotalPrice { get; set; }
            public DateTime CreateDate { get; set; }
            public int ClientId { get; set; }
        }
    }
}