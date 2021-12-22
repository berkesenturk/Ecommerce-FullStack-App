using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EcommerceApi.Data;
using EcommerceApi.Entities;

namespace EcommerceApi.Operations.VendorOperations.Query
{
        public class GetVendorsQuery
        {
            private readonly ApplicationDbContext _dbContext;

            public GetVendorsQuery(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }


            public List<VendorsViewModel> Handle()
            {
                var VendorList = _dbContext.Vendors.OrderBy(x => x.Id).ToList<Vendor>(); 
                List<VendorsViewModel> vm = new List<VendorsViewModel>();

                foreach (var Vendor in VendorList)
                {
                    vm.Add(new VendorsViewModel()
                    {
                        Id = Vendor.Id,
                        VendorName = Vendor.VendorName,
                        FollowerCount = Vendor.FollowerCount,
                        Rating = Vendor.Rating,

                    });
                }
                return vm;
        }       

        public class VendorsViewModel
        {
            public int Id { get; set; }
            public string VendorName { get; set; }
            public int FollowerCount { get; set; }
            public double Rating { get; set; }
        }
   }
}