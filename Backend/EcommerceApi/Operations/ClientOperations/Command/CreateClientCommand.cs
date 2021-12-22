using System;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Entities;

namespace EcommerceApi.Operations.ClientOperations.Command
{
    public class CreateClientCommand
    {
        public CreateClientModel Model { get; set; }
        private readonly ApplicationDbContext _dbContext;
        
        public CreateClientCommand(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var client = _dbContext.Clients.SingleOrDefault(x => x.Phone == Model.Phone);

            if(client is not null)
                throw new InvalidOperationException("category exists already.");

            client = new Client();
            client.FirstName = Model.FirstName;
            client.LastName = Model.LastName;
            client.Phone = Model.Phone;
            client.AddressLine = Model.AddressLine;
            client.City = Model.City;
            client.PostalCode = Model.PostalCode;
            
            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();
            
        }


    }
    public class CreateClientModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}