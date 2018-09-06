using AutoMapper;
using Shop.Common;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using Shop.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Shop.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        IProductCategoryService _productCategoryService;
        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }
        // GET: Product
        public ActionResult Detail(int id)
        {
            var productModel = _productService.GetById(id);
            var viewmodel = Mapper.Map<ProductViewModel>(productModel);

            var relatedProduct = _productService.GetReatedProducts(id, 6);
            if (relatedProduct.Count() > 0)
            {
                ViewBag.RelatedProducts = Mapper.Map<IEnumerable<ProductViewModel>>(relatedProduct);
            }
            else
            {
                ViewBag.RelatedProducts = new List<ProductViewModel>();
            }

            if (viewmodel.MoreImages != null)
            {
                List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(viewmodel.MoreImages);
                ViewBag.MoreImages = listImages;
            }
            else
            {
                ViewBag.MoreImages = new List<string>();
            }

            var tags =_productService.GetListTagByProductID(id);
            if (tags.Count() > 0)
            {
                ViewBag.Tags = Mapper.Map<IEnumerable<TagViewModel>>(tags);
            }
            else
            {
                ViewBag.Tags = new List<TagViewModel>();
            }
            return View(viewmodel);
        }

        public ActionResult Category(int id, int page =1, string sortBy = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("pageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByCategoryIdPaging(id,page,pageSize, sortBy, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<ProductViewModel>>(productModel);
            int totalPages =(int)Math.Ceiling(((double)totalRow / pageSize));
            ViewBag.Category = Mapper.Map<ProductCategoryViewModel>(_productCategoryService.GetById(id));

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("maxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPages
            };
            return View(paginationSet);
        }

        public ActionResult Search(string keyword, int page = 1, string sortBy = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("pageSize"));
            int totalRow = 0;
            var productModel = _productService.Search(keyword, page, pageSize, sortBy, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<ProductViewModel>>(productModel);
            int totalPages = (int)Math.Ceiling(((double)totalRow / pageSize));
            ViewBag.Keyword = keyword;

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("maxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPages
            };
            return View(paginationSet);
        }
        public ActionResult ListByTag(string tagid, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("pageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByTag(tagid, page, pageSize, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<ProductViewModel>>(productModel);
            int totalPages = (int)Math.Ceiling(((double)totalRow / pageSize));
            ViewBag.Tag = Mapper.Map<TagViewModel>(_productService.GetTag(tagid));

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("maxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPages
            };
            return View(paginationSet);
        }
        public JsonResult GetListProductByName(string name)
        {
            var model = _productService.GetListProductByName(name);
            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }
    }
}