using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppForWebshop.Data;
using WebAppForWebshop.Models;



namespace WebAppForWebshop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;



        public OrderController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Checkout(string _CartId)
        {
            CartAndProduct model = new CartAndProduct();
            model.listForCartAndProduct = (from cart in _db.ShoppingCartItems
                                           join product in _db.Products on cart.ProductId equals product.Id
                                           into Details
                                           from m in Details.DefaultIfEmpty()
                                           select new CartAndProduct
                                           {
                                               CartId = _CartId,
                                               ProductId = m.Id,
                                               ProductName = m.Name,
                                               ProductImage = m.ProductImage,
                                               PricePerItem = m.Price,
                                               Quantity = cart.Quantity,
                                               TotalPrice = m.Price * cart.Quantity,
                                               DateCreated = cart.DateCreated,
                                               ItemId = cart.ItemId
                                           }).ToList();
            return View(model.listForCartAndProduct);
        }



        public IActionResult Receipt()
        {
            return View();
        }



        public IActionResult OrderHistory()
        {
            return View();
        }
    }
}