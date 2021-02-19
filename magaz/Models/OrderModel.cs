using CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace magaz.Models
{
    public class OrderModel
    {
        protected DataContext dataContext;
        public OrderModel()
        {
            this.dataContext = new DataContext();
        }
        public bool MakeOrder(Order order, int user_id, int shop_id,int prod_id)
        {
            order.Product = dataContext.Products.Find(prod_id);
            order.Shop = dataContext.Shops.Find(shop_id);
            order.Account = dataContext.ApplicationUser.Find(user_id);
            order.Status = "Заказано";
            order.Date = DateTime.Now;
            Shop_Prod shop = dataContext.Shop_Prods.FirstOrDefault(i => (i.Product.ID == prod_id) && (i.Shop.ID == shop_id));
            if (shop.Quantity < order.Quantity)
            { return false; }
            else
            {
                shop.Quantity = shop.Quantity - order.Quantity;
                dataContext.Orders.Add(order);
                dataContext.Carts.FirstOrDefault(i => i.ID == order.Account.Cart.ID).Products.Remove(order.Product);
                dataContext.SaveChanges();
                return true;
            }
        }
        public List<Product> GetBest()
        {
            return dataContext.Orders.Where(i => i.Date.Day > (DateTime.Now.Day - 7)).Select(j=>j.Product).Take(5).ToList();

        }
      
    }
}