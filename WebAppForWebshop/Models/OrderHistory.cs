using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppForWebshop.Models
{
    public class OrderHistory
    {
        public List<OrderHistory> listOrderHistory = new List<OrderHistory>();
        public List<OrderHistory> finalHistoryList = new List<OrderHistory>();
        public int OrderId { get; set; }

        public string CustomerId { get; set; }

        public string CustomerUsername { get; set; } //email or username of customer
        public string DeliveryAddress { get; set; }
        
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
