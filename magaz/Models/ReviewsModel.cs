using CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace magaz.Models
{
    public class ReviewsModel
    {
        protected DataContext dataContext;
        public ReviewsModel()
        {
            this.dataContext = new DataContext();
        }
    }
}