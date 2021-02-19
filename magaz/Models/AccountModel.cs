using CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace magaz.Models
{
    public class AccountModel
    {


        protected DataContext dataContext;
        public AccountModel()
        {
            this.dataContext = new DataContext();
        }

       /* public List<Account> GetListAccount()
        {
            return dataContext.Accounts.ToList();
        }
       public void AddAccount(Account account)
        {
            dataContext.Accounts.Add(account);
            dataContext.SaveChanges();
        }
        public Account GetAccByID(int id)
        {
            return dataContext.Accounts.FirstOrDefault(i => i.ID == id);
        }
  */
 
    }
}