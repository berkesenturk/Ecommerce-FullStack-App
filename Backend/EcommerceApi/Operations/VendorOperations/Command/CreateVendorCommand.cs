using System;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Entities;

namespace EcommerceApi.Operations.VendorOperations.Command
{
    public class CreateVendorCommand
    {
        public CreateVendorModel Model { get; set; }
        private readonly ApplicationDbContext _dbContext;
        
        public CreateVendorCommand(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var Vendor = new Vendor();
            Vendor.VendorName = Model.VendorName;
            Vendor.FollowerCount = Model.FollowerCount;
            Vendor.Rating = Model.Rating;
            
            _dbContext.Vendors.Add(Vendor);
            _dbContext.SaveChanges();
            
        }


    }
    public class CreateVendorModel
    {
        public string VendorName { get; set; }
        public int FollowerCount { get; set; }
        public double Rating { get; set; }
    }
}