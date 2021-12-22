using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Entities;

namespace EcommerceApi.Operations.OrderDetailOperations.Query
{
        public class GetOrderDetailsQuery
        {
            private readonly ApplicationDbContext _dbContext;

            public GetOrderDetailsQuery(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public List<OrderDetailsViewModel> Handle()
            {
                var OrderDetailList = _dbContext.OrderDetails.OrderBy(x => x.Id).ToList<OrderDetail>();  
                List<OrderDetailsViewModel> vm = new List<OrderDetailsViewModel>();   

                var clientList = _dbContext.Clients.OrderBy(x => x.Id).ToList<Client>();
                List<ClientsViewModel> clientVm = new List<ClientsViewModel>();

                foreach (var c in clientList)
                {
                    clientVm.Add(new ClientsViewModel()
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Phone = c.Phone,
                        AddressLine = c.AddressLine,
                        City = c.City,
                        PostalCode = c.PostalCode
                    });
                }

                foreach (var OrderDetail in OrderDetailList)
                {
                    vm.Add(new OrderDetailsViewModel()
                    {
                        Id = OrderDetail.Id,
                        TotalPrice = OrderDetail.TotalPrice,
                        CreateDate = OrderDetail.CreateDate,
                        PaymentDetails = OrderDetail.PaymentDetails,
                        Client = clientVm.Where(v => v.Id == OrderDetail.ClientId).FirstOrDefault()
                    });
                }
                return vm;
            }
        }

        public class OrderDetailsViewModel
        {
            public int Id { get; set; }
            public decimal TotalPrice { get; set; }
            public DateTime CreateDate { get; set; }
            public ICollection<PaymentDetail> PaymentDetails  { get; set; }
            public ClientsViewModel Client { get; set; }
        }
        public class ClientsViewModel
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string AddressLine { get; set; }
            public string City { get; set; }
            public string PostalCode { get; set; }
        }
}