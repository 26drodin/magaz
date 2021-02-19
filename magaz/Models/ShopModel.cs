using CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace magaz.Models
{
    public class ShopModel
    {
        protected DataContext dataContext;
        public ShopModel()
        {
            this.dataContext = new DataContext();
        }
        //API AIzaSyBAhSm5CyMCkIaL4cTqO9CYW56MZr5Au2Q

        public List<Shop> GetListShop()
        {
            return dataContext.Shops.ToList();
        }
        public void AddShop(Shop shop)
        {
            dataContext.Shops.Add(shop);
            dataContext.SaveChanges();
        }
        public void Remove(int id)
        {
            Shop shop = dataContext.Shops.Find(id);

            dataContext.Shop_Prods.RemoveRange(shop.Shop_Prods);
            dataContext.Orders.RemoveRange(dataContext.Orders.Where(i => i.Shop.ID == shop.ID));
            dataContext.Shops.Remove(shop);
            dataContext.SaveChanges();
        }
        internal Shop GetShopByID(int id)
        {
            return dataContext.Shops.FirstOrDefault(i => i.ID == id);
        }
        public Shop_Prod CheckProd(int prod_id,int shop_id)
        {
            return dataContext.Shop_Prods.FirstOrDefault(i => (i.Product.ID == prod_id) && (i.Shop.ID == shop_id));
        }

        public void AddProd(Shop_Prod prod,int shop_id,int product_id)
        {
            prod.Shop =dataContext.Shops.Find(shop_id);
            prod.Product = dataContext.Products.Find(product_id);
            dataContext.Shop_Prods.Add(prod);
            dataContext.SaveChanges();
        }

        public List<Shop_Prod> GetProds(int shop_id)
        {
            return dataContext.Shop_Prods.Where(i => i.Shop.ID == shop_id).ToList();
        }
        public Shop_Prod GetProd(int shop_prod_id)
        {
            return dataContext.Shop_Prods.Find(shop_prod_id);
        }
        public void EditProds(Shop_Prod prod, int shop_id, int product_id)
        {
            if (prod.Quantity == 0)
            {
                dataContext.Shop_Prods.Remove(dataContext.Shop_Prods.Find(prod.ID));
                dataContext.SaveChanges();
            }
            else
            {
                prod.Shop = dataContext.Shops.Find(shop_id);
                prod.Product = dataContext.Products.Find(product_id);
                dataContext.Entry(prod).State = EntityState.Modified;

                dataContext.SaveChanges();
            }
        }
    }
}