using CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace magaz.Models
{
    public class ProductModel
    {
        protected DataContext dataContext;
        public ProductModel()
        {
            this.dataContext = new DataContext();
        }
        public List<Product> GetListProd()
        {
            return dataContext.Products.ToList();
        }
        public void AddProd(Product product,HttpPostedFileBase image,int? cat_ID)
        {
            if (image != null)
            {
                product.Picture = new byte[image.ContentLength];
                image.InputStream.Read(product.Picture, 0, image.ContentLength);
            }          
            if (cat_ID != null)
            {
                product.Category = dataContext.Categories.Find(cat_ID);
            }
            dataContext.Products.Add(product);
            dataContext.SaveChanges();
        }
        public void Edit(Product product, HttpPostedFileBase image)
        {
            if (image != null)
            {
                product.Picture = new byte[image.ContentLength];
                image.InputStream.Read(product.Picture, 0, image.ContentLength);
            }
            Product pr = dataContext.Products.Find(product.ID);

            pr.Name = product.Name;
            pr.Picture = product.Picture;
            pr.Price = product.Price;
                     
           dataContext.Entry(pr).State = EntityState.Modified;
            dataContext.SaveChanges();
        }

        public void CreateProps(int prod_id)
        {   
            List<Prop_Cat> a=dataContext.Props_Cat.Where(b=>b.Category== dataContext.Products.FirstOrDefault(j=>j.ID==prod_id).Category).ToList();
         
            foreach (var item in a)
            {
                dataContext.Props_Prod.Add(new Prop_Prod { Name=item.Name,Product=dataContext.Products.Find(prod_id)});
                dataContext.SaveChanges();
            }
        }

        public Product GetProduct(int id)
        {
            return dataContext.Products.Find(id);
           
        }

        public void EditProps(List<Prop_Prod> props,int ProdId)
        {foreach (var item in props)
            {
                item.Product = dataContext.Products.Find(ProdId);
                dataContext.Entry(item).State = EntityState.Modified;             
            }
            dataContext.SaveChanges();
        }


        public List<Prop_Prod> GetProps(int id)
        {
          
            return dataContext.Props_Prod.Where(aa=> aa.Product == dataContext.Products.FirstOrDefault(i=>i.ID==id)).ToList();
        }

        public List<Product> GetProdByCat(int id)
        {
            return dataContext.Products.Where(a => a.Category == dataContext.Categories.FirstOrDefault(i => i.ID == id)).ToList();
        }
        public SelectList Droplist()
        {
            return (new SelectList(dataContext.Categories, "ID", "Name"));
        }


        public List<string> GetPropsNames(int cat_id)
        {
            return dataContext.Props_Prod.Where(i => i.Product.Category.ID == cat_id).Select(j => j.Name).Distinct().ToList();
        }
        public List<List<string>> GetPropsValues(List<String> values,int cat_id)
        {
            string b;
            List < List<string> > a=new List<List<string>>();
            for (int i = 0; i < values.Count; i++)
            {
                b = values[i];
                a.Add(dataContext.Props_Prod.Where(j => j.Name == b&&j.Product.Category.ID==cat_id).Select(f => f.Value).Distinct().ToList());
            }
            return a;
        }

        public List<Shop_Prod> GetShops(int prod_id)
        {
            return dataContext.Shop_Prods.Where(i => i.Product.ID == prod_id).ToList();
        }

        public List<Shop> GetShopsNames(int prod_id)
        {
            return dataContext.Shop_Prods.Where(i => i.Product.ID == prod_id).Select(j=>j.Shop).ToList();
        }


        public List<Product> Search(string name,int cat_id)
        {


            if(cat_id==0)
                return dataContext.Products.Where(i => i.Name.Contains(name)).ToList();
            return dataContext.Products.Where(i=>i.Name.Contains(name)&&i.Category.ID==cat_id).ToList();

        }

        public List<Product> Search(string name)
        {
            return dataContext.Products.Where(i => i.Name.Contains(name)).ToList();

        }
        public void DeleteProd(Product product)
        {
            dataContext.Orders.RemoveRange(dataContext.Orders.Where(i=>i.Product.ID==product.ID));
            dataContext.Props_Prod.RemoveRange(dataContext.Props_Prod.Where(i => i.Product.ID == product.ID));
            dataContext.Shop_Prods.RemoveRange(dataContext.Shop_Prods.Where(i => i.Product.ID == product.ID));
            dataContext.Reviews.RemoveRange(dataContext.Reviews.Where(i => i.Product.ID == product.ID));
            dataContext.Products.Remove(product);
            dataContext.SaveChanges();
        }
        public void AddReview(string text,int user_id,int prod_id,short rate)
        {
            ApplicationUser account = dataContext.ApplicationUser.Find(user_id);        
            Product product = dataContext.Products.Find(prod_id);
            dataContext.Reviews.Add(new Review() { Date = DateTime.Now, Text = text, Account = account,Product=product,Rate=rate });
            dataContext.SaveChanges();
        }

        public List<Product> Filter(Dictionary<string,List<string>> filters, int cat_id)
        {
            List<Product> list = dataContext.Products.Where(i=> i.Category.ID == cat_id).ToList();
            List<Product> tmp = new List<Product>();

            foreach (var keys in filters.Keys)
            {
                foreach (string item in filters[keys])
                { tmp.AddRange(list.Where(i => i.Props.FirstOrDefault(j => (j.Name == keys) && (j.Value == item)) != null).ToList());
                   
                }
                
                list.Clear();
                list.AddRange(tmp);             
                tmp.Clear();
            }

     

            return list.OrderBy(i => i.Price).ToList();
        }


    }
}






