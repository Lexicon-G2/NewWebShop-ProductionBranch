using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppForWebshop.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        
        public  ApplicationUser Customer { get; set; }
    }
}
