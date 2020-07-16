using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppForWebshop.Models
{
    public class SpecificOrderHistory
    {
        public List<SpecificOrderHistory> listSpecificOrderHistory = new List<SpecificOrderHistory>();
        //public List<OrderHistory> finalHistoryList = new List<OrderHistory>();
        public int OrderId { get; set; }

        public string CustomerId { get; set; }

        public string CustomerUsername { get; set; } //email or username of customer
        public string DeliveryAddress { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal PricePerItem { get; set; }
        public decimal TotalPrice { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string ProductImage { get; set; }

        public int orefId { get; set; }
    }
}
