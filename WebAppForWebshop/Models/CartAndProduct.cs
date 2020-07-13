using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppForWebshop.Models
{
    public class CartAndProduct
    {
        
            public List<CartAndProduct> listForCartAndProduct = new List<CartAndProduct>();

        public string CartId { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal PricePerItem { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateCreated { get; set; }
            
        public string ItemId { get; set; }    
            
            
    }
}
