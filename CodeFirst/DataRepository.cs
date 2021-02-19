using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{



    public class ApplicationUser : IdentityUser<int, ApplicationUserUserLogin,
       AplicationUserRole, AplicationUserClaim>
    {
        public string Password { get; set; }
        [DisplayName("ФИО")]
        public string FIO { get; set; }
        public virtual List<Discount> Discounts { get; set; }
        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        public virtual List<Review> Reviews { get; set; }

        public virtual List<Order> Orders { get; set; }

        [DisplayName("Комменты")]
        public virtual List<Comment> Comments { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual List<Roles> Roles { get; set; }


    }
    public class ApplicationUserUserLogin : IdentityUserLogin<int>
    {
        [Key]
        public override int UserId { get => base.UserId; set => base.UserId = value; }
    }
    public class AplicationUserRole : IdentityUserRole<int>
    {
        [Key]
        public override int UserId { get => base.UserId; set => base.UserId = value; }
        public int User { get; set; }    }
    public class AplicationUserClaim : IdentityUserClaim<int>
 
    {
        [Key]
        public override int UserId { get => base.UserId; set => base.UserId = value; }
    }
    public class Roles
   {
        [Key]
        public int ID { get; set; }
        [DisplayName("Роль")]
        public string role { get; set; }
        public virtual List<ApplicationUser> Users { get; set; }
    }






















    /*

    [DisplayName("Аккаунт")]
    public class Account
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Логин")]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        public string Password { get; set; }

        [DisplayName("ФИО")]
        public string FIO { get; set; }

        public virtual List<Discount> Discounts { get; set; }

        public virtual List<Review> Reviews { get; set; }

        public virtual List<Order> Orders { get; set; }

        [DisplayName("Комменты")]
        public virtual List<Comment> Comments { get; set; }

        public virtual Cart Cart { get; set; }

        [DisplayName("Дата регистрации")]
        public DateTime Date { get; set; }

    }*/
    [DisplayName("Бонусная карта")]
    public class Discount
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Бонусы")]
        public Double Bonus { get; set; }

        [DisplayName("Номер Карты")]
        public string Card_num { get; set; }

        public virtual ApplicationUser Account { get; set; }

        [DisplayName("Дата оформления")]
        public DateTime Date { get; set; }

    }

    public class Cart
    {
        [Key]
        public int ID { get; set; }

        public virtual List<Product> Products { get; set; }
    }


    public class Order
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Количество")]
        public int Quantity { get; set; }

        [DisplayName("Статус")]
        public string Status { get; set; }

        [DisplayName("Дата заказа")]
        public DateTime Date { get; set; }
        [DisplayName("Товар")]
        public virtual Product Product { get; set; }
        [Required]
        [DisplayName("Магазин")]
        public virtual Shop Shop { get; set; }
        [DisplayName("Пользователь")]
        public virtual ApplicationUser Account { get; set; }

    }


    public class Product
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Название")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Цена")]
        [Required]
        public double Price { get; set; }

        [Column(TypeName="image")]
        public byte[] Picture { get; set; }

        [DisplayName("Категория")]
        public virtual Category Category { get; set; }
        public virtual List<Cart> Carts { get; set; }
        public virtual List<Review> Reviews { get; set; }
        public virtual List<Prop_Prod> Props { get; set; }

        

    }




    public class Category
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        public virtual List<Prop_Cat> Props { get; set; }
      //  public virtual Product Product {get ;set; }
     
    }

    public class Shop
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Адрес")]
        public string Address { get; set; }

        public string Coord_N { get; set; }

        public string Coord_E { get; set; }

        public virtual List<Shop_Prod> Shop_Prods { get; set; }
    }
    public class Prop_Cat
    {
        [Key]
        public int ID { get; set; }
        
        [DisplayName("Название")]
        public string Name { get; set; }
        public virtual Category Category { get; set; }
    }
    public class Prop_Prod
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Значение")]
        public string Value { get; set; }
        public virtual Product Product { get; set; }

       
    }

    public class Shop_Prod
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Магазин")]
        public virtual Shop Shop { get; set; }

        [DisplayName("Товар")]
        public virtual Product Product { get; set; }

        [DisplayName("Количество")]
        public int Quantity { get; set; }
    }
    public class Review
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Оценка")]
        public short Rate { get; set; }

        [DisplayName("Текст комментария")]
        public string Text { get; set; }

        public virtual ApplicationUser Account { get; set; }

        public virtual Product Product { get; set; }

        [DisplayName("Дата")]
        public DateTime Date { get; set; }

    }
    public class Comment
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Текст комментария")]
        public string Text { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        public virtual ApplicationUser Account { get; set; }
        public virtual News News { get; set; }
    }
    public class News
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        [Column(TypeName = "image")]
        [DisplayName("Превью")]
        public byte[] Picture { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        [DisplayName("Комменты")]
        public virtual List<Comment> Comments { get; set; }
    }
 }
