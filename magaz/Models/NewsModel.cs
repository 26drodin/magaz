using CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace magaz.Models
{
    public class NewsModel
    {
        protected DataContext dataContext;
        public NewsModel()
        {
            this.dataContext = new DataContext();
        }
        public List<News> GetList()
        {
            return dataContext.News.ToList();
        }
        public News GetNews(int id)
        {
            return dataContext.News.Find(id);
        }
        public void Remove(int id)
        {
            News news = dataContext.News.Find(id);
            dataContext.Comments.Where(i => i.News.ID == news.ID);
            dataContext.SaveChanges();
        }

   /*     public List<string> GetPreview(News news)
        {
            List<string> a = new List<string>();
            a = dataContext.News.Select(i => i.Text).ToList();
            foreach (var item in a)
item.ToString().Substring(0, 100);
            return a;
        }*/
        public void AddNews(News news, HttpPostedFileBase image)
        {
            if (image != null)
            {
                news.Picture = new byte[image.ContentLength];
                image.InputStream.Read(news.Picture, 0, image.ContentLength);
            }

            news.Date = DateTime.Now;
            dataContext.News.Add(news);
            dataContext.SaveChanges();
        }
        public void Edit(News news, HttpPostedFileBase image)
        {
            if (image != null)
            {
                news.Picture = new byte[image.ContentLength];
                image.InputStream.Read(news.Picture, 0, image.ContentLength);
            }
            news.Date = DateTime.Now;
            dataContext.Entry(news).State = EntityState.Modified;
            dataContext.SaveChanges();
        }
        public void DeleteNews(News news)
        {
            dataContext.News.Remove(news);
            dataContext.SaveChanges();
        }
        public List<Comment> GetComments(int id)
        {
            return dataContext.News.Find(id).Comments.ToList();
        }
        public void AddComment(int news_id,string text, int usr_id)
        {
            var news = dataContext.News.Find(news_id);
            var acc = dataContext.ApplicationUser.Find(usr_id);
            news.Comments.Add(new Comment { Text = text, Account = acc, News = news, Date = DateTime.Now });
            dataContext.SaveChanges();
        }
    }

}