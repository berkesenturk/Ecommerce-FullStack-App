using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Entities;

namespace EcommerceApi.Operations.ClientOperations.Query
{
        public class GetClientsQuery
        {
            private readonly ApplicationDbContext _dbContext;

            public GetClientsQuery(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }


            public List<ClientsViewModel> Handle()
            {
                var clientList = _dbContext.Clients.OrderBy(x => x.Id).ToList<Client>(); 
                List<ClientsViewModel> vm = new List<ClientsViewModel>();

                foreach (var client in clientList)
                {
                    vm.Add(new ClientsViewModel()
                    {
                        Id = client.Id,
                        FirstName = client.FirstName,
                        LastName = client.LastName,
                        Phone = client.Phone,
                        AddressLine = client.AddressLine,
                        City = client.City,
                        PostalCode = client.PostalCode,
                        OrderDetails = client.OrderDetails

                    });
                }
                return vm;
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
            public ICollection<OrderDetail> OrderDetails { get; set; }            
        }
   }
}