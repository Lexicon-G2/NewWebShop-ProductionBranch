using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppForWebshop.Models
{
    public class OrderReference
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Orders OrderId { get; set; }
       
        [Required]
        public Products ProductId { get; set; }

        public decimal Price { get; set; }
    }
}
