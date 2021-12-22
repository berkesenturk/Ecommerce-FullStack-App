using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Operations.PaymentDetailOperations.Query;
using EcommerceApi.Operations.PaymentDetailOperations.Command;
using EcommerceApi.Entities;
using static EcommerceApi.Operations.PaymentDetailOperations.Command.CreatePaymentDetailCommand;

namespace EcommerceApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class PaymentDetailController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public PaymentDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            GetPaymentDetailsQuery query = new GetPaymentDetailsQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public PaymentDetail GetById(int id)
        {
            var PaymentDetail = _context.PaymentDetails.Where(PaymentDetail => PaymentDetail.Id == id).SingleOrDefault();
            return PaymentDetail;
        }

        //Post
        [HttpPost]
        public IActionResult AddUser([FromBody] CreatePaymentDetailModel newPaymentDetail)
        {
            CreatePaymentDetailCommand command = new CreatePaymentDetailCommand(_context);

            try
            {
                command.Model = newPaymentDetail;
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
        public IActionResult UpdateUser(int id, [FromBody] PaymentDetail updatedPaymentDetail)
        {
            var PaymentDetail = _context.PaymentDetails.SingleOrDefault(x => x.Id == id);

            if (PaymentDetail == null)
                return BadRequest();

            PaymentDetail.Amount = updatedPaymentDetail.Amount != default ? updatedPaymentDetail.Amount : PaymentDetail.Amount;
            PaymentDetail.Type = updatedPaymentDetail.Type != default ? updatedPaymentDetail.Type : PaymentDetail.Type;
            PaymentDetail.Provider = updatedPaymentDetail.Provider != default ? updatedPaymentDetail.Provider : PaymentDetail.Provider;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var PaymentDetail = _context.PaymentDetails.SingleOrDefault(x => x.Id == id);
            if (PaymentDetail == null)
                return BadRequest();

            _context.PaymentDetails.Remove(PaymentDetail);
            _context.SaveChanges();
            return Ok();
        }
    }
}