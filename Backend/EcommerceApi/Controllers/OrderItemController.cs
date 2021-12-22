using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Operations.OrderItemOperations.Query;
using EcommerceApi.Operations.OrderItemOperations.Command;
using EcommerceApi.Entities;
using static EcommerceApi.Operations.OrderItemOperations.Command.CreateOrderItemCommand;

namespace EcommerceApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class OrderItemController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public OrderItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            GetOrderItemsQuery query = new GetOrderItemsQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public OrderItem GetById(int id)
        {
            var OrderItem = _context.OrderItems.Where(OrderItem => OrderItem.Id == id).SingleOrDefault();
            return OrderItem;
        }

        //Post
        [HttpPost]
        public IActionResult AddUser([FromBody] CreateOrderItemModel newOrderItem)
        {
            CreateOrderItemCommand command = new CreateOrderItemCommand(_context);

            try
            {
                command.Model = newOrderItem;
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
        public IActionResult UpdateUser(int id, [FromBody] OrderItem updatedOrderItem)
        {
            var OrderItem = _context.OrderItems.SingleOrDefault(x => x.Id == id);

            if (OrderItem == null)
                return BadRequest();

            OrderItem.Quantity = updatedOrderItem.Quantity != default ? updatedOrderItem.Quantity : OrderItem.Quantity;
            OrderItem.CreateDate = updatedOrderItem.CreateDate != default ? updatedOrderItem.CreateDate : OrderItem.CreateDate;
            OrderItem.ProductId = updatedOrderItem.ProductId != default ? updatedOrderItem.ProductId : OrderItem.ProductId;
            OrderItem.OrderDetailId = updatedOrderItem.OrderDetailId != default ? updatedOrderItem.OrderDetailId : OrderItem.OrderDetailId;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var OrderItem = _context.OrderItems.SingleOrDefault(x => x.Id == id);
            if (OrderItem == null)
                return BadRequest();

            _context.OrderItems.Remove(OrderItem);
            _context.SaveChanges();
            return Ok();
        }
    }
}