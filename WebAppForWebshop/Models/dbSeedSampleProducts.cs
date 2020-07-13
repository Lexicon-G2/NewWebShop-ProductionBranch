using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppForWebshop.Data;

namespace WebAppForWebshop.Models
{
    public class dbSeedSampleProducts
    {
        private ApplicationDbContext _context;

        public dbSeedSampleProducts(ApplicationDbContext context)
        {
            this._context = context;
            
        }

        public async void SeedProducts() {

            var banana = new Products() { Name ="Banana", Category ="Fruit", Price =10, ProductImage = "https://i.imgur.com/oYFwcbB.jpg" };
            var apple = new Products() {  Name = "Apple", Category = "Fruit", Price = 10, ProductImage = "https://i.imgur.com/tEJbRok.jpg" };
            var lemon = new Products() {  Name = "Lemon", Category = "Fruit", Price = 10, ProductImage = "https://i.imgur.com/WxuTQX3.jpg" };

            var dog = new Products() {  Name = "Dog", Category = "Pets", Price = 25, ProductImage = "https://i.picsum.photos/id/237/50/50.jpg?hmac=9cCVRLgc5HmY_XbEZ4SSgnaR5CqTMUtHPZ04MCvtH-k" };
            var cat = new Products() {  Name = "Cat", Category = "Pets", Price = 21, ProductImage = "https://i.imgur.com/6itqCfE.jpg" };
            var rabbit = new Products() { Name = "Rabbit", Category = "Pets", Price = 16, ProductImage = "https://i.imgur.com/gxp1tE8.jpg" };

            var phone = new Products() {  Name = "Phone", Category = "Devices", Price = 30, ProductImage = "https://i.imgur.com/PvPKNcD.jpg" };
            var pc = new Products() {  Name = "PC", Category = "Devices", Price = 100, ProductImage = "https://i.imgur.com/SBHmnzu.jpg" };
            var laptop = new Products() {  Name = "Laptop", Category = "Devices", Price = 50, ProductImage = "https://i.imgur.com/G7nYe2Y.jpg" };

            if (!_context.Products.Any(u => u.Name == dog.Name)) { 
                _context.Products.AddRange(banana, apple, lemon, dog, cat, rabbit, phone, pc, laptop);
            }
           await _context.SaveChangesAsync();

        }

        
    }
}
