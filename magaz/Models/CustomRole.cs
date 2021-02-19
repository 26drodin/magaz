using CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace magaz.Models
{
    public class CustomRole
    {      
                protected DataContext dataContext;
                public CustomRole()
                {
                    this.dataContext = new DataContext();
                }
        public void CreateRole(string role)
        {
            dataContext.Roles.Add(new Roles { role = role });
            dataContext.SaveChanges();
        }
        public void AddToRole(string role, int usr_id)
        {
            var Role = dataContext.Roles.FirstOrDefault(i => i.role == role);
            dataContext.ApplicationUser.Find(usr_id).Roles.Add(Role);
            dataContext.SaveChanges();
        }
        public void RemoveRoles(int usr_id)
        {
            dataContext.ApplicationUser.Find(usr_id).Roles.Clear();
            dataContext.SaveChanges();
        }
        public void RemoveFromRole(string role, int usr_id)
        {
            var Role = dataContext.Roles.FirstOrDefault(i => i.role == role);
            dataContext.ApplicationUser.Find(usr_id).Roles.Remove(Role);
            dataContext.SaveChanges();
        }


        public List<Roles> AllRoles()
        {
            return dataContext.Roles.ToList();
        }


        public List<string> GetRoles(int user_id)
        {  if (user_id == 0) return new List<string>();
            return dataContext.ApplicationUser.Find(user_id).Roles.Select(i => i.role).ToList();
        }
        public bool IsInRole(int user_id,string role)
        {
            if (user_id == 0) return false;
            return dataContext.ApplicationUser.Find(user_id).Roles.Select(i => i.role).Contains(role);
        }
        //public bool IsInRole(string role_name, int user_id)
        //{
        //    Roles role = dataContext.Roles.FirstOrDefault(i => i.Name == role_name);
        //    return dataContext.AplicationUserRole.FirstOrDefault(j=>(j.RoleId==role.Id)&&(j.UserId==user_id)) != null;
        //}

    }
}