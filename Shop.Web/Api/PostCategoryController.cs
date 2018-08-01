using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        IPostCategoryService _postCategoryService;
        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }
        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return base.CreateHttpResponse(request, () =>
            {
                var listAll = _postCategoryService.GetAll();
                HttpResponseMessage response = null;
                response = request.CreateResponse(HttpStatusCode.OK, listAll);
                return response;
            });
        }
        public HttpResponseMessage Post(HttpRequestMessage request, PostCategory postCategory)
        {
            return base.CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var postAdd = _postCategoryService.Add(postCategory);
                    _postCategoryService.Savechanges();
                    response = request.CreateResponse(HttpStatusCode.Created, postAdd);
                }
                return response;
            });
        }

        public HttpResponseMessage Put(HttpRequestMessage request, PostCategory postCategory)
        {
            return base.CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _postCategoryService.Update(postCategory);
                    _postCategoryService.Savechanges();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return base.CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var postDelete = _postCategoryService.Delete(id);
                    _postCategoryService.Savechanges();
                    response = request.CreateResponse(HttpStatusCode.Created, postDelete);
                }
                return response;
            });
        }


    }
}