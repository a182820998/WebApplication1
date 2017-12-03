using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models.ClientData;

namespace WebApplication1.DataBase
{
    //Initial default data
    public class StoreUtil : DropCreateDatabaseIfModelChanges<StoreContext>
    {
        protected override void Seed(StoreContext context)
        {
            //Intital customers
            var customers = new List<Customer>
            {
                new Customer{Name="Jackson Lin",Password = "jackson0502",Email="jackson123458@gmail.com",RegisterOn=DateTime.Parse("2017-11-15")},
                new Customer{Name="Sheng-Wei Lin",Password = "angel444555",Email="a182820998@gmail.com",RegisterOn=DateTime.Parse("2017-11-17")},
                new Customer{Name="Jenny Chen",Password = "juckeo5699",Email="jenny_8502136@gmail.com",RegisterOn=DateTime.Parse("2017-11-20")},
                new Customer{Name="Andy Lou",Password = "assholea123456",Email="jackandy5566@gmail.com",RegisterOn=DateTime.Parse("2017-11-23")},
            };
            customers.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();

            //Initial products
            var products = new List<Product>
            {
                new Product{ProductId=1050,Name="Chemistry",Price=369,},
                new Product{ProductId=4022,Name="Microeconomics",Price=399,},
                new Product{ProductId=4041,Name="Macroeconomics",Price=269,},
                new Product{ProductId=1045,Name="Calculus",Price=199,},
                new Product{ProductId=3141,Name="Trigonometry",Price=1590,},
                new Product{ProductId=2021,Name="Composition",Price=29999,},
                new Product{ProductId=2042,Name="Literature",Price=3469,}
            };
            products.ForEach(s => context.Products.Add(s));
            context.SaveChanges();

            //Initial orders
            var orders = new List<Order>
            {
                new Order{CustomerId=1,ProductId=1050,Memo="memo11"},
                new Order{CustomerId=1,ProductId=4022,Memo="memo12"},
                new Order{CustomerId=1,ProductId=4041,Memo="memo13"},
                new Order{CustomerId=2,ProductId=1045,Memo="memo14"},
                new Order{CustomerId=2,ProductId=3141,Memo="memo15"},
                new Order{CustomerId=2,ProductId=2021,Memo="memo16"},
                new Order{CustomerId=3,ProductId=1050},
                new Order{CustomerId=4,ProductId=1050},
                new Order{CustomerId=4,ProductId=4022,Memo="memo17"},

            };
            orders.ForEach(s => context.Orders.Add(s));
            context.SaveChanges();
        }
    }
}