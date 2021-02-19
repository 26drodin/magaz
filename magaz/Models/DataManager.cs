using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace magaz.Models
{
    public class DataManager
    {
        private AccountModel _AccountModel;
        public AccountModel AccountModel
        {
            get
            {
                if (_AccountModel == null)
                {
                    _AccountModel = new AccountModel();
                }
                return _AccountModel;
            }
        }

        private CategoryModel _CategoryModel;
        public CategoryModel CategoryModel
        {
            get
            {
                if (_CategoryModel == null)
                {
                    _CategoryModel = new CategoryModel();
                }
                return _CategoryModel;
            }
        }

        private ShopModel _ShopModel;
        public ShopModel ShopModel
        {
            get
            {
                if (_ShopModel == null)
                {
                    _ShopModel = new ShopModel();
                }
                return _ShopModel;
            }
        }

        private ProductModel _ProductModel;
        public ProductModel ProductModel
        {
            get
            {
                if (_ProductModel == null)
                {
                    _ProductModel = new ProductModel();
                }
                return _ProductModel;
            }
        }

        private NewsModel _NewsModel;
        public NewsModel NewsModel
        {
            get
            {
                if (_NewsModel == null)
                {
                    _NewsModel = new NewsModel();
                }
                return _NewsModel;
            }
        }

        private OrderModel _OrderModel;
        public OrderModel OrderModel
        {
            get
            {
                if (_OrderModel == null)
                {
                    _OrderModel = new OrderModel();
                }
                return _OrderModel;
            }
        }

        private ReviewsModel _ReviewsModel;
        public ReviewsModel ReviewsModel
        {
            get
            {
                if (_ReviewsModel == null)
                {
                    _ReviewsModel = new ReviewsModel();
                }
                return _ReviewsModel;
            }
        }

        private CustomUserStore _CustomUserStore;
        public CustomUserStore CustomUserStore
        {
            get
            {
                if (_CustomUserStore == null)
                {
                    _CustomUserStore = new CustomUserStore();
                }
                return _CustomUserStore;
            }
        }

        private CartModel _CartModel;
        public CartModel CartModel
        {
            get
            {
                if (_CartModel == null)
                {
                    _CartModel = new CartModel();
                }
                return _CartModel;
            }
        }

        private CustomRole _CustomRole;
        public CustomRole CustomRole
        {
            get
            {
                if (_CustomRole == null)
                {
                    _CustomRole = new CustomRole();
                }
                return _CustomRole;
            }
        }

    }
}