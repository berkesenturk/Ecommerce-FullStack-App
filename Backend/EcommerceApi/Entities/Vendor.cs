using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Entities
{
    public class Vendor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string VendorName { get; set; }
        public int FollowerCount { get; set; }
        public double Rating { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}