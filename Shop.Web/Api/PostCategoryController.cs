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
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        IPostCategoryService _postCategoryService;
        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return base.CreateHttpResponse(request, () =>
            {
                var listPostCategory = _postCategoryService.GetAll();
                var listPostCategoryVM = Mapper.Map<List<PostCategoryViewModel>>(listPostCategory);
                HttpResponseMessage response = null;
                response = request.CreateResponse(HttpStatusCode.OK, listPostCategoryVM);
                return response;
            });
        }
        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
        {
            return base.CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    PostCategory newPostCategory = new PostCategory();
                    newPostCategory.UpdatePostCategory(postCategoryVM);
                    var postCategory = _postCategoryService.Add(newPostCategory);
                    _postCategoryService.Savechanges();

                    var responseData = Mapper.Map<PostCategory, PostCategoryViewModel>(postCategory);

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }
        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
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
                    var postCategoryDb = _postCategoryService.GetById(postCategoryVM.ID);
                    postCategoryDb.UpdatePostCategory(postCategoryVM);
                    _postCategoryService.Update(postCategoryDb);
                    _postCategoryService.Savechanges();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }
        [Route("delete")]
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