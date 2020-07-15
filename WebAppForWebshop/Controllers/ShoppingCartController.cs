using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppForWebshop.Data;
using WebAppForWebshop.Models;



namespace WebAppForWebshop.Controllers
{
    [Authorize]



    public class ShoppingCartController : Controller
    {



        public string ShoppingCartId { get; set; }



        private readonly ApplicationDbContext _db;



        public const string CartSessionKey = "CartId";



        private readonly IHttpContextAccessor _httpContextAccessor;





        public ShoppingCartController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext db)
        {
            this._httpContextAccessor = httpContextAccessor;



            this._db = db;
        }





        //get shopping Cart
        public IActionResult Index()
        {



            ShoppingCartId = GetCartId();
            CartAndProduct model = new CartAndProduct();
            model.listForCartAndProduct = (from cart in _db.ShoppingCartItems
                                           join product in _db.Products on cart.ProductId equals product.Id
                                           into Details
                                           from m in Details.DefaultIfEmpty()
                                           select new CartAndProduct
                                           {
                                               CartId = ShoppingCartId,
                                               ProductId = m.Id,
                                               ProductName = m.Name,
                                               ProductImage = m.ProductImage,
                                               PricePerItem = m.Price,
                                               Quantity = cart.Quantity,
                                               TotalPrice = m.Price * cart.Quantity,
                                               DateCreated = cart.DateCreated,
                                               ItemId = cart.ItemId
                                           }).ToList();

            //return View("Index", _db.ShoppingCartItems.Where(
            //    c => c.CartId == ShoppingCartId).ToList());

            return View("Index", model.listForCartAndProduct);

        }


        public async Task<IActionResult> Delete(string? id)
        {
            var item = await _db.ShoppingCartItems.FindAsync(id);
            _db.ShoppingCartItems.Remove(item);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult AddToCart(int id)
        {
            // Retrieve the product from the database. 
            ShoppingCartId = GetCartId();





            var cartItem = _db.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == id);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = id,
                    CartId = ShoppingCartId,
                    Product = _db.Products.SingleOrDefault(
                   p => p.Id == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };





                _db.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.Quantity++;
            }
            _db.SaveChanges();




            return RedirectToAction("Index");
            //return View();
        }



        // deleting all items in the shopping Cart
        public IActionResult EmptyCart()
        {
            ShoppingCartId = GetCartId();
            var cartItems = _db.ShoppingCartItems.Where(
                c => c.CartId == ShoppingCartId);
            foreach (var cartItem in cartItems)
            {
                _db.ShoppingCartItems.Remove(cartItem);
            }
            // Save changes.             
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public string GetCartId()
        {





            if (HttpContext.Session.GetString("CartSessionKey") == null)
            {
                if (!string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
                {
                    HttpContext.Session.SetString("CartSessionKey", _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                }
                else
                {
                    // Generate a new random GUID using System.Guid class.     
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Session.SetString("CartSessionKey", tempCartId.ToString());
                }
            }
            return HttpContext.Session.GetString("CartSessionKey").ToString();
        }


        public IActionResult ChangeAmount(string id, string button)
        {
            var item = _db.ShoppingCartItems.Find(id);
            if (button == "+")
                item.Quantity++;
            else
                item.Quantity--;

            if (item.Quantity < 1)
                _db.ShoppingCartItems.Remove(item);
            else
                _db.Update(item);

            _db.SaveChanges();

            return RedirectToAction("Index");
        }


        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId();





            return _db.ShoppingCartItems.Where(
                c => c.CartId == ShoppingCartId).ToList();
        }







    }
}