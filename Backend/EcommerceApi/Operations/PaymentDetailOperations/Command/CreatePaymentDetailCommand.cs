using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Entities;

namespace EcommerceApi.Operations.PaymentDetailOperations.Command
{
    public class CreatePaymentDetailCommand
    {
        public CreatePaymentDetailModel Model { get; set; }
        private readonly ApplicationDbContext _dbContext;
        
        public CreatePaymentDetailCommand(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var PaymentDetail = new PaymentDetail();
            PaymentDetail.Type = Model.Type;
            PaymentDetail.Amount = Model.Amount;
            PaymentDetail.Provider = Model.Provider; 
            PaymentDetail.OrderDetailId = Model.OrderDetailId;

            _dbContext.PaymentDetails.Add(PaymentDetail);
            _dbContext.SaveChanges();
            
        }

        public class CreatePaymentDetailModel
        {
            public string Type { get; set; }
            [Column(TypeName = "decimal(18,2)")]
            public decimal Amount { get; set; }
            public string Provider { get; set; }
            public int OrderDetailId { get; set; }
        }
    }
}