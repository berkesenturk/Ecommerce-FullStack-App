using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Operations.VendorOperations.Query;
using EcommerceApi.Operations.VendorOperations.Command;
using EcommerceApi.Entities;
using static EcommerceApi.Operations.VendorOperations.Command.CreateVendorCommand;

namespace EcommerceApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class VendorController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public VendorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetVendors()
        {
            GetVendorsQuery query = new GetVendorsQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public Vendor GetById(int id)
        {
            var Vendor = _context.Vendors.Where(Vendor => Vendor.Id == id).SingleOrDefault();
            return Vendor;
        }

        //Post
        [HttpPost]
        public IActionResult AddVendor([FromBody] CreateVendorModel newVendor)
        {
            CreateVendorCommand command = new CreateVendorCommand(_context);

            try
            {
                command.Model = newVendor;
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
        public IActionResult UpdateBook(int id, [FromBody] Vendor updatedVendor)
        {
            var Vendor = _context.Vendors.SingleOrDefault(x => x.Id == id);

            if (Vendor == null)
                return BadRequest();

            Vendor.VendorName = updatedVendor.VendorName != default ? updatedVendor.VendorName : Vendor.VendorName;
            Vendor.FollowerCount = updatedVendor.FollowerCount != default ? updatedVendor.FollowerCount : Vendor.FollowerCount;
            Vendor.Rating = updatedVendor.Rating != default ? updatedVendor.Rating : Vendor.Rating;

            _context.SaveChanges();
            return Ok();    
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var Vendor = _context.Vendors.SingleOrDefault(x => x.Id == id);
            if (Vendor == null)
                return BadRequest();

            _context.Vendors.Remove(Vendor);
            _context.SaveChanges();
            return Ok();
        }
    }
}