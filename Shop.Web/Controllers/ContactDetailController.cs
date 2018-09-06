using AutoMapper;
using BotDetect.Web.Mvc;
using Shop.Common;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Extensions;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    public class ContactDetailController : Controller
    {
        IContactDetailService _contactDetailService;
        public ContactDetailController(IContactDetailService contactDetailService)
        {
            this._contactDetailService = contactDetailService;
        }
        // GET: ContactDetail
        public ActionResult Index()
        {
            var model = _contactDetailService.GetById(2);
            var contactVM = Mapper.Map<ContactDetailViewModel>(model);
            return View(contactVM);
        }

        // GET: ContactDetail
        public ActionResult PhanHoi()
        {
            return View(new ContactDetailViewModel());
        }
        // Post: ContactDetail
        [HttpPost]
        [CaptchaValidation("CaptchaCode", "contactcapcha", "Incorrect CAPTCHA code!")]
        public ActionResult SendContactDetail(ContactDetailViewModel ctvm)
        {
            if (ModelState.IsValid)
            {
                ContactDetail ct = new ContactDetail();
                ct.UpdateContactDetail(ctvm);
                _contactDetailService.Create(ct);
                _contactDetailService.save();
                ViewData["SeccessMsg"] = "Send thanh cong";
                
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/templates/thongtinlienhe.html"));
                content = content.Replace("{{Name}}", ctvm.Name);
                content = content.Replace("{{Email}}", ctvm.Email);
                content = content.Replace("{{Other}}", ctvm.Other);

                MailHelper.SendMail(ctvm.Email, "Lien he moi", content);
            }
            return View("PhanHoi", ctvm);
        }
    }
}