using CodeFirst;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace magaz.IdentityFolder
{
    public class CustomUserValidator: IIdentityValidator<ApplicationUser>
    {
        public async Task<IdentityResult> ValidateAsync(ApplicationUser item)
        {
            List<string> errors = new List<string>();


            DataContext data = new DataContext();

            if (data.ApplicationUser.FirstOrDefault(i => i.UserName == item.UserName) != null)
                errors.Add("Имя уже занято");

            if (data.ApplicationUser.FirstOrDefault(i => i.Email == item.Email) != null)
                errors.Add("Почта уже занята");

            if (String.IsNullOrEmpty(item.UserName.Trim()))
                errors.Add("Вы указали пустое имя.");


      //      if (String.IsNullOrEmpty(item.PhoneNumber.Trim()))
         //       errors.Add("Вы не указали телефонный номер.");

            string userNamePattern = @"^[a-zA-Z0-9а-яА-Я]+$";

            if (!Regex.IsMatch(item.UserName, userNamePattern))
                errors.Add("В имени разрешается указывать буквы английского или русского языков, и цифры");


            if (errors.Count > 0)
                return IdentityResult.Failed(errors.ToArray());

            return IdentityResult.Success;
        }
    }
}