using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppForWebshop.Models
{
    public class Receipt
    {
        public string CustomerId { get; set; }

        public string CustomerUsername { get; set; } //email or username of customer
        public string DeliveryAddress { get; set; }
        //include product x price, for every single product,
        //how to do this? Like the print below (like a real receipt)

        /*
         Products like:

        Product A * Quantity 3 = A.price * 3 -> totalPrice
        Product B * Quantity 2 = B.price * 2 -> TotalPrice
        */
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
