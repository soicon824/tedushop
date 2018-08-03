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
        public HttpResponseMessage Get(HttpRequestMessage request)
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
        //[Route("add")]
        //public HttpResponseMessage Post(HttpRequestMessage request, ProductCategoryViewModel ProductCategoryVM)
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
        //            ProductCategory newProductCategory = new ProductCategory();
        //            newProductCategory.UpdateProductCategory(ProductCategoryVM);
        //            var ProductCategory = _ProductCategoryService.Add(newProductCategory);
        //            _ProductCategoryService.Savechanges();
        //            response = request.CreateResponse(HttpStatusCode.Created, ProductCategory);
        //        }
        //        return response;
        //    });
        //}
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