using AutoMapper;
using Microsoft.AspNet.Identity;
using Shop.Service;
using Shop.Web.App_Start;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Shop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        IProductService _productService;
        ApplicationUserManager _userManager;
        public ShoppingCartController(IProductService productService, ApplicationUserManager userManager)
        {
            this._productService = productService;
            _userManager = userManager;
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            if (Session[Common.CommonConstants.SessionCart] == null)
            {
                Session[Common.CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            }
            return View();
        }

        // GET: ShoppingCart
        public ActionResult CheckOut()
        {
            if (Session[Common.CommonConstants.SessionCart] == null)
            {
                return Redirect("/gio-hang.html");
            }
            return View();
        }

        // GET: ShoppingCart
        public ActionResult GetUser()
        {
            if (Request.IsAuthenticated)
            {
                var user = _userManager.FindById(User.Identity.GetUserId());
                return Json(new
                {
                    data = user,
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        [HttpPost]
        public JsonResult Add(int productId)
        {
            var cart = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];
            if (cart == null)
            {
                cart = new List<ShoppingCartViewModel>();
            }
            if (cart.Any(x => x.ProductId == productId))
            {
                foreach (var item in cart)
                {
                    if (item.ProductId == productId)
                    {
                        item.Quantity += 1;
                    }
                }
            }
            else
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel();
                newItem.ProductId = productId;
                var product = _productService.GetById(productId);
                newItem.Product = Mapper.Map<ProductViewModel>(product);
                newItem.Quantity = 1;
                cart.Add(newItem);
            }
            Session[Common.CommonConstants.SessionCart] = cart;
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var cartVM = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);
            var cartSession = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];

            foreach (var item in cartSession)
            {
                foreach (var item2 in cartVM)
                {
                    if (item.ProductId == item2.ProductId)
                    {
                        item.Quantity = item2.Quantity;
                    }
                }
            }
            Session[Common.CommonConstants.SessionCart] = cartSession;
            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var cart = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];
            if (cart == null)
            {
                return Json(new
                {
                    status = false
                });
            }
            return Json(new
            {
                status = true,
                data = cart
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[Common.CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            return Json(new
            {
                status = true
            });
        }
        [HttpPost]
        public JsonResult DeleteItem(int productId)
        {
            var cart = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];
            if (cart != null)
            {
                cart.RemoveAll(x => x.ProductId == productId);
                Session[Common.CommonConstants.SessionCart] = cart;
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }
    }
}