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
    [RoutePrefix("api/contactdetail")]
    public class ContactDetailController : ApiControllerBase
    {
        IContactDetailService _contactDetailService;
        private IErrorService _errorService;
        public ContactDetailController(IContactDetailService contactDetailService,
            IErrorService errorService) : base(errorService)
        {
            this._contactDetailService = contactDetailService;
            this._errorService = errorService;
        }
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, int page, int pageSize)
        {
            return base.CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var query = _contactDetailService.GetAllPaging(page, pageSize, out totalRow);
                var listContactDetail = Mapper.Map<List<ContactDetailViewModel>>(query);
                PaginationSet<ContactDetailViewModel> paginationSet = new PaginationSet<ContactDetailViewModel>()
                {
                    Items = listContactDetail,
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
                var ct = _contactDetailService.GetById(id);
                var ctvm = Mapper.Map<ContactDetailViewModel>(ct);

                HttpResponseMessage response = null;
                response = request.CreateResponse(HttpStatusCode.OK, ctvm);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ContactDetailViewModel ctvm)
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
                    ContactDetail newctdt = new ContactDetail();
                    newctdt.UpdateContactDetail(ctvm);
                    var ct = _contactDetailService.Create(newctdt);
                    _contactDetailService.save();

                    var responseData = Mapper.Map<ContactDetailViewModel>(ct);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ContactDetailViewModel ContactDetailVM)
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
                    //var contactDetailCategoryDb = _ContactDetailService.GetById(ContactDetailVM.ID);
                    var contactDetailDb = new ContactDetail();
                    contactDetailDb.UpdateContactDetail(ContactDetailVM);
                    _contactDetailService.Update(contactDetailDb);
                    _contactDetailService.save();
                    var responseData = Mapper.Map<ContactDetailViewModel>(contactDetailDb);
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
                    var oldContactDetail = _contactDetailService.Delete(id);
                    _contactDetailService.save();

                    var responseData = Mapper.Map<ContactDetail, ContactDetailViewModel>(oldContactDetail);
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
                        _contactDetailService.Delete(item);
                    }
                    _contactDetailService.save();
                    response = request.CreateResponse(HttpStatusCode.Created, true);
                }

                return response;
            });
        }

    }
}