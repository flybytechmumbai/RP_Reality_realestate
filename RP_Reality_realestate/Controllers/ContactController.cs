using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using RP_Reality_realestate.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Xml.Linq;
using System.Web.Helpers;


namespace RP_Reality_realestate.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult ContactView()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(ContactMail mContactMail)
        {
            if (ModelState.IsValid)
            {
                // Send email to admin
                string adminEmail = "nikitasingh@flybytech.co"; // Replace with your admin's email
                string adminSubject = "New Registration Form Submission";
                string adminBody = GetEmailBody(mContactMail);

                // Call the SendEmail method through the instance
                ContactMail.SendEmail(adminEmail, adminSubject, adminBody);

                // Send email to user
                string userSubject = "Thank you for Registering!";
                string userBody = "Thank you for registering. We will get back to you soon! Please check your details: \n" + GetEmailBody(mContactMail);

                // Call the SendEmail method to send email
                ContactMail.SendEmail(mContactMail.Email, userSubject, userBody);

                // Additional processing if needed
                return Json(new { success = true, message = "Registration successful!" });
            }
            return Json(new { success = false, message = "Invalid form data. Please check and try again." });
        }
        private string GetEmailBody(ContactMail mContactMail)
        {
            return $"Name: {mContactMail.Name}\n" +
                   $"Email: {mContactMail.Email}\n" +
                   $"Phone: {mContactMail.Phone}\n" +
                   $"Msg: {mContactMail.Msg}\n";
        }

    }
}