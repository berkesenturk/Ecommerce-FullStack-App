using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApi.Operations.PaymentDetailOperations.Query
{
        public class GetPaymentDetailsQuery
        {
            private readonly ApplicationDbContext _dbContext;

            public GetPaymentDetailsQuery(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public List<PaymentDetailsViewModel> Handle()
            {
                var PaymentDetailList = _dbContext.PaymentDetails.OrderBy(x => x.Id).ToList<PaymentDetail>();  
                List<PaymentDetailsViewModel> vm = new List<PaymentDetailsViewModel>();   


                foreach (var pd in PaymentDetailList)
                {
                    vm.Add(new PaymentDetailsViewModel()
                    {
                        Amount = pd.Amount,
                        Type = pd.Type,
                        Provider = pd.Provider,
                        OrderDetailId = pd.OrderDetailId
                    });
                }
                return vm;
            }
        }

        public class PaymentDetailsViewModel
        {
            [Column(TypeName = "decimal(18,2)")]
            public decimal Amount { get; set; }
            public string Type { get; set; }
            public string Provider { get; set; }
            public int OrderDetailId { get; set; }
        }
}