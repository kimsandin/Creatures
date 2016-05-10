using ClashOfTheCharacters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ClashOfTheCharacters.Controllers
{
    public class HelpController : Controller
    {
        // GET: Help
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult Gameguide()
        {
            return View();
        }

        [HttpGet]
        public ViewResult ContactForm()
        {
            return View();
        }
        [HttpPost]
        public ViewResult ContactForm(ContactRequest contactRequest)
        {
            if (ModelState.IsValid)
            {
                var customerName = contactRequest.Name;
                var customerEmail = contactRequest.Email;
                var customerRequest = contactRequest.SupportRequest;
                var customerPhone = contactRequest.Phone;
                //TODO: Email question to support
                try
                {
                    WebMail.SmtpServer = " send.one.com";
                    WebMail.SmtpPort = 465;
                    WebMail.EnableSsl = true;
                    WebMail.UserName = "creatures@cimoco.se";
                    WebMail.Password = "123456";
                    WebMail.From = "creatures@cimoco.se";
                    WebMail.Send(to: customerEmail, subject: "Help request from - " + customerName,
                        body: customerRequest + customerPhone


                    );
                    return View("Thanks", contactRequest);
                }
                catch (Exception)
                {
                    return View("SendEmailError");
                }
            }
            else
            {
                // there is a validation error
                return View();
            }

        }
    }
}