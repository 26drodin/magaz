using CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace magaz.Models
{
    public class CategoryModel
    {

        protected DataContext dataContext;
        public CategoryModel()
        {
            this.dataContext = new DataContext();
        }
        public void AddCategory(Category category)
        {
            dataContext.Categories.Add(category);
            dataContext.SaveChanges();
        }
        public void DeleteCategory(Category category)
        {
            dataContext.Categories.Remove(category);
            dataContext.SaveChanges();
        }
        public void AddProp(int cat_id,string name)
        {
 
            dataContext.Props_Cat.Add(new Prop_Cat { Category = GetCat(cat_id), Name=name});
            List<Product> updated = dataContext.Products.Where(i => i.Category == dataContext.Categories.FirstOrDefault(z=>z.ID==cat_id)).ToList();
            foreach (var item in updated)
            {
                dataContext.Props_Prod.Add(new Prop_Prod { Name = name, Product = item });
            }

            dataContext.SaveChanges();
        }


        public void DeleteProp(int prop_id)
        {


            Prop_Cat prop = dataContext.Props_Cat.Find(prop_id);
    
            //return dataContext.Group.FirstOrDefault(i => i.Id == id);
           
            List<Product> updated = dataContext.Products.Where((i=>i.Props.Select(j=>j.Name).Contains(prop.Name)&&(i.Category.ID==prop.Category.ID))).ToList();
            foreach (var item in updated)
            {
                dataContext.Props_Prod.Remove(dataContext.Props_Prod.FirstOrDefault(i => i.Name == prop.Name));
            }
            dataContext.Props_Cat.Remove(prop);
            dataContext.SaveChanges();
        }



        public List<Category> GetListCat()
        {
            return dataContext.Categories.ToList();
        }
        internal Category GetCat(int id)
        {
            return dataContext.Categories.FirstOrDefault(i => i.ID == id);
        }
        public void Edit(Category category) {
            dataContext.Entry(category).State = EntityState.Modified;
            dataContext.SaveChanges();
        }

        public List<Prop_Cat> GetProps(int id)
        {
            return dataContext.Props_Cat.Where(a => a.Category== dataContext.Categories.FirstOrDefault(i => i.ID == id)).ToList();
        }
  

    }
}