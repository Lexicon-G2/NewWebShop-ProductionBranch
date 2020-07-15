﻿using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebAppForWebshop.Data;
using WebAppForWebshop.Models;



namespace WebAppForWebshop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        private UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderController(ApplicationDbContext db, UserManager<ApplicationUser> userMrg, IHttpContextAccessor httpContextAccessor)
        {
            this._db = db;
            userManager = userMrg;
            this._httpContextAccessor = httpContextAccessor;
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



        public async Task<IActionResult> Receipt(string _CartId)
        {
            CartAndProduct model = new CartAndProduct();
           // var user = await userManager.FindByIdAsync(User.Identity.GetType());
            var user = await userManager.GetUserAsync(HttpContext.User);
            var user1 = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
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
            var insUser = (ApplicationUser)_db.Users.Where(x => x.Id == user1).FirstOrDefault();

            var order = new Orders()
            {
                OrderDate = DateTime.Now,
                Customer = insUser
            };
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            var orderid = order.Id;
            foreach (var item in model.listForCartAndProduct)
            {
                int count = item.Quantity;
                while (count > 0) {
                    count--;
                var orderref = new OrderReference()
                {
                    OrderId = order,
                    ProductId = _db.Products.Where(p => p.Id == item.ProductId).FirstOrDefault(),
                    Price = item.PricePerItem
                };
                await _db.OrderReferences.AddAsync(orderref);
            }
            }
            await _db.SaveChangesAsync();
            return View(model.listForCartAndProduct);
        }

        /*
        
            model.listForCartAndProduct = (from cart in _db.ShoppingCartItems
                                           join product in _db.Products on cart.ProductId equals product.Id
                                           into Details
                                           from m in Details.DefaultIfEmpty()
                                           select new CartAndProduct
                                           {


         * var result = from t1 in someContext.Table1
              join t2 in someContext.Table2 on 
        new { SomeID = t1.SomeID, SomeName = someName} equals 
                          new { SomeID = t2.SomeID, SomeName = t2.SomeName}
        */

        //public IActionResult OrderHistory()
        //{
        //    OrderHistory model = new OrderHistory();
        //    model.listOrderHistory = (from user in _db.Users
        //                              join order in _db.Orders
        //                              on user.Id equals order.Customer.Id
        //                              into Details
        //                              from m in Details.DefaultIfEmpty()
        //                              select new OrderHistory
        //                              {

        //                              }).ToList();

        //    return View();
        //}

        //public IActionResult OrderHistory1()
        //{
        //    OrderHistory model = new OrderHistory();
        //    model.listOrderHistory = (from order in _db.Orders
        //                              join o in _db.OrderReferences
        //                              on new { id = order.Id } equals new {id = o.OrderId }
        //                              into j
        //                              select new OrderHistory { }).ToList();

        //    return View("OrderHistory");
        //}

        public async Task<IActionResult> OrderHistory()
        {
            OrderHistory model = new OrderHistory();
            ApplicationUser tempUser = new ApplicationUser();
            model.listOrderHistory = (from oref in _db.OrderReferences
                                            join order in _db.Orders on oref.OrderId.Id
                                            equals order.Id
                                            join user in _db.Users on order.Customer.Id equals user.Id
                                            into Details 
                                            from m in Details.DefaultIfEmpty() 
                                            select new OrderHistory
                                            {
                                                OrderId = oref.OrderId.Id,
                                                CustomerId = m.Id,
                                                CustomerUsername = m.UserName, 
                                                OrderDate = order.OrderDate,
                                                TotalPrice = oref.Price
                                            }).ToList();
            
            foreach (var item in model.listOrderHistory)
            {
                var user1 = (ApplicationUser)await _db.Users.Where(x => x.Id == item.CustomerId).FirstOrDefaultAsync();
                item.DeliveryAddress = user1.Address;
               
            }

            var tempo = new OrderHistory();

            foreach (var item in model.listOrderHistory)
            {
                tempo = new OrderHistory()
                {
                    OrderId = item.OrderId,
                    OrderDate = item.OrderDate,
                    CustomerId = item.CustomerId,
                    CustomerUsername = item.CustomerUsername,
                    DeliveryAddress = item.DeliveryAddress,
                    TotalPrice = item.TotalPrice
                };
                
                break;
                
            }

            //var temp = new OrderHistory();
            //var temp = model.listOrderHistory.FirstOrDefault();
            tempo.TotalPrice = tempo.TotalPrice - tempo.TotalPrice;
            foreach (var item in model.listOrderHistory)
            {

                if (tempo.OrderId != item.OrderId)
                {

                    model.finalHistoryList.Add(tempo);
                    tempo = new OrderHistory(){
                        OrderId = item.OrderId,
                        OrderDate = item.OrderDate,
                        CustomerId = item.CustomerId,
                        CustomerUsername = item.CustomerUsername,
                        DeliveryAddress = item.DeliveryAddress,
                        TotalPrice = item.TotalPrice
                    };
                }
                else
                {

                    tempo.TotalPrice = tempo.TotalPrice + item.TotalPrice;
                }

            }
            model.finalHistoryList.Add(tempo);

            return View("OrderHistory", model);
        }

        public IActionResult ManageOrder(int id)
        {
            return View();
        }

    }
}