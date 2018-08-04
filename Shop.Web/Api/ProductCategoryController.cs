using AutoMapper;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using Shop.Web.Infrastructure.Extensions;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        IProductCategoryService _ProductCategoryService;
        public ProductCategoryController(IErrorService errorService, IProductCategoryService ProductCategoryService) : base(errorService)
        {
            this._ProductCategoryService = ProductCategoryService;
        }
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request,string keyword, int page, int pageSize)
        {
            return base.CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var listProductCategory = _ProductCategoryService.GetAll(keyword);
                totalRow = listProductCategory.Count();
                var query = listProductCategory.OrderByDescending(x=>x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var listProductCategoryVM = Mapper.Map<List<ProductCategoryViewModel>>(query);
                PaginationSet<ProductCategoryViewModel> paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = listProductCategoryVM,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                
                HttpResponseMessage response = null;
                response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }
        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAllParents(HttpRequestMessage request)
        {
            return base.CreateHttpResponse(request, () =>
            {
                var listProductCategory = _ProductCategoryService.GetAll();
                var listProductCategoryVM = Mapper.Map<List<ProductCategoryViewModel>>(listProductCategory);
                
                HttpResponseMessage response = null;
                response = request.CreateResponse(HttpStatusCode.OK, listProductCategoryVM);
                return response;
            });
        }
        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel ProductCategoryVM)
        {
            return base.CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    ProductCategory newProductCategory = new ProductCategory();
                    newProductCategory.UpdateProductCategory(ProductCategoryVM);
                    var productCategory = _ProductCategoryService.Add(newProductCategory);
                    _ProductCategoryService.Savechanges();

                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        //[Route("update")]
        //public HttpResponseMessage Put(HttpRequestMessage request, ProductCategoryViewModel ProductCategoryVM)
        //{
        //    return base.CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        if (ModelState.IsValid)
        //        {
        //            request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            var ProductCategoryDb = _ProductCategoryService.GetById(ProductCategoryVM.ID);
        //            ProductCategoryDb.UpdateProductCategory(ProductCategoryVM);
        //            _ProductCategoryService.Update(ProductCategoryDb);
        //            _ProductCategoryService.Savechanges();
        //            response = request.CreateResponse(HttpStatusCode.OK);
        //        }
        //        return response;
        //    });
        //}
        //[Route("delete")]
        //public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        //{
        //    return base.CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        if (ModelState.IsValid)
        //        {
        //            request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            var postDelete = _ProductCategoryService.Delete(id);
        //            _ProductCategoryService.Savechanges();
        //            response = request.CreateResponse(HttpStatusCode.Created, postDelete);
        //        }
        //        return response;
        //    });
        //}


    }
}