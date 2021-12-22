using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApi.Entities
{
    public class OrderDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<PaymentDetail> PaymentDetails { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}