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
using System.Web.Script.Serialization;

namespace Shop.Web.Api
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiControllerBase
    {
        IProductService _productCategoryService;
        public ProductController(IErrorService errorService, IProductService ProductService) : base(errorService)
        {
            this._productCategoryService = ProductService;
        }
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request,string keyword, int page, int pageSize)
        {
            return base.CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var listProduct = _productCategoryService.GetAll(keyword);
                totalRow = listProduct.Count();
                var query = listProduct.OrderByDescending(x=>x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var listProductVM = Mapper.Map<List<ProductViewModel>>(query);
                PaginationSet<ProductViewModel> paginationSet = new PaginationSet<ProductViewModel>()
                {
                    Items = listProductVM,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                
                HttpResponseMessage response = null;
                response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return base.CreateHttpResponse(request, () =>
            {
                var productCategory = _productCategoryService.GetById(id);
                var productCategoryVM = Mapper.Map<ProductViewModel>(productCategory);

                HttpResponseMessage response = null;
                response = request.CreateResponse(HttpStatusCode.OK, productCategoryVM);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel ProductVM)
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
                    Product newProduct = new Product();
                    newProduct.UpdateProduct(ProductVM);
                    var productCategory = _productCategoryService.Add(newProduct);
                    _productCategoryService.Savechanges();

                    var responseData = Mapper.Map<Product, ProductViewModel>(productCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel ProductVM)
        {
            return base.CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    //var productCategoryDb = _ProductService.GetById(ProductVM.ID);
                    var productCategoryDb = new Product();
                    productCategoryDb.UpdateProduct(ProductVM);
                    _productCategoryService.Update(productCategoryDb);
                    _productCategoryService.Savechanges();
                    var responseData = Mapper.Map<ProductViewModel>(productCategoryDb);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldProduct = _productCategoryService.Delete(id);
                    _productCategoryService.Savechanges();

                    var responseData = Mapper.Map<Product, ProductViewModel>(oldProduct);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string lstId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var ids = new JavaScriptSerializer().Deserialize<List<int>>(lstId);
                    foreach (var item in ids)
                    {
                        _productCategoryService.Delete(item);
                    }
                    _productCategoryService.Savechanges();
                    response = request.CreateResponse(HttpStatusCode.Created, true);
                }

                return response;
            });
        }

    }
}