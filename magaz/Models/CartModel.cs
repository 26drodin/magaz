using CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

namespace magaz.Models
{
    public class CartModel
    {
        protected DataContext dataContext;
        public CartModel()
        {
            this.dataContext = new DataContext();
        }


        public void AddCart(int user_id)
        {
            Cart cart = new Cart();
            dataContext.Carts.Add(cart);      
            dataContext.ApplicationUser.Find(user_id).Cart=cart;
            dataContext.SaveChanges();
           
        }
        public void Addprod(int user_id,int prod_id)
        {
            Product product = dataContext.Products.Find(prod_id);
            
          //  int user_id = Convert.ToInt32(a.User.Identity.GetUserId());
            product.Carts.Add(dataContext.ApplicationUser.Find(user_id).Cart);
            dataContext.Entry(product).State = EntityState.Modified;
            dataContext.SaveChanges(); 
        }
        public void ClearCart(int user_id)
        {
           Cart cart = dataContext.ApplicationUser.Find(user_id).Cart;
            cart.Products.RemoveAll(i=>i.Carts.FirstOrDefault(j=>j.ID==cart.ID)==cart);
            dataContext.Entry(cart).State = EntityState.Modified;
           
            dataContext.SaveChanges();
        }
        public void DeleteCart(Cart cart)
        {
            dataContext.Carts.Remove(cart);
            dataContext.SaveChanges();
        }
    }
}