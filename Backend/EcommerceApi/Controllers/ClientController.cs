using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Operations.ClientOperations.Query;
using EcommerceApi.Operations.ClientOperations.Command;
using EcommerceApi.Entities;
using static EcommerceApi.Operations.ClientOperations.Command.CreateClientCommand;

namespace EcommerceApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class ClientController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            GetClientsQuery query = new GetClientsQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public Client GetById(int id)
        {
            var client = _context.Clients.Where(client => client.Id == id).SingleOrDefault();
            return client;
        }

        //Post
        [HttpPost]
        public IActionResult AddUser([FromBody] CreateClientModel newClient)
        {
            CreateClientCommand command = new CreateClientCommand(_context);

            try
            {
                command.Model = newClient;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        //Put
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] Client updatedClient)
        {
            var client = _context.Clients.SingleOrDefault(x => x.Id == id);

            if (client == null)
                return BadRequest();

            client.FirstName = updatedClient.FirstName != default ? updatedClient.FirstName : client.FirstName;
            client.LastName = updatedClient.LastName != default ? updatedClient.LastName : client.LastName;
            client.Phone = updatedClient.Phone != default ? updatedClient.Phone : client.Phone;
            client.AddressLine = updatedClient.AddressLine != default ? updatedClient.AddressLine : client.AddressLine;
            client.City = updatedClient.City != default ? updatedClient.City : client.City;
            client.PostalCode = updatedClient.PostalCode != default ? updatedClient.PostalCode : client.PostalCode;

            _context.SaveChanges();
            return Ok();    
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var client = _context.Clients.SingleOrDefault(x => x.Id == id);
            if (client == null)
                return BadRequest();

            _context.Clients.Remove(client);
            _context.SaveChanges();
            return Ok();
        }
    }
}