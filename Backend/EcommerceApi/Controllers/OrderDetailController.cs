using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Operations.OrderDetailOperations.Query;
using EcommerceApi.Operations.OrderDetailOperations.Command;
using EcommerceApi.Entities;
using static EcommerceApi.Operations.OrderDetailOperations.Command.CreateOrderDetailCommand;

namespace EcommerceApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class OrderDetailController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public OrderDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            GetOrderDetailsQuery query = new GetOrderDetailsQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public OrderDetail GetById(int id)
        {
            var OrderDetail = _context.OrderDetails.Where(OrderDetail => OrderDetail.Id == id).SingleOrDefault();
            return OrderDetail;
        }

        //Post
        [HttpPost]
        public IActionResult AddUser([FromBody] CreateOrderDetailModel newOrderDetail)
        {
            CreateOrderDetailCommand command = new CreateOrderDetailCommand(_context);

            try
            {
                command.Model = newOrderDetail;
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
        public IActionResult UpdateUser(int id, [FromBody] OrderDetail updatedOrderDetail)
        {
            var OrderDetail = _context.OrderDetails.SingleOrDefault(x => x.Id == id);

            if (OrderDetail == null)
                return BadRequest();

            OrderDetail.TotalPrice = updatedOrderDetail.TotalPrice != default ? updatedOrderDetail.TotalPrice : OrderDetail.TotalPrice;
            OrderDetail.CreateDate = updatedOrderDetail.CreateDate != default ? updatedOrderDetail.CreateDate : OrderDetail.CreateDate;
            OrderDetail.ClientId = updatedOrderDetail.ClientId != default ? updatedOrderDetail.ClientId : OrderDetail.ClientId;
            
            _context.SaveChanges();
            return Ok();    
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var OrderDetail = _context.OrderDetails.SingleOrDefault(x => x.Id == id);
            if (OrderDetail == null)
                return BadRequest();

            _context.OrderDetails.Remove(OrderDetail);
            _context.SaveChanges();
            return Ok();
        }
    }
}