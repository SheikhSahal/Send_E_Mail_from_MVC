using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Send_email.Models;
using System.Threading.Tasks;

namespace Send_email.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendEmail(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<h2>Welcome to the Big Bang Store</h2>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("s.m.sahal786@outlook.com"));
                /*message.To.Add(new MailAddress("s.m.sahal789@gmail.com"));*/  // replace with valid value 
                message.From = new MailAddress("s.m.sahal786@outlook.com");  // replace with valid value

                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "no.reply.Big.Bang@outlook.com",  // replace with valid value
                        Password = "bigbangstore123"  // replace with valid value2
                        //UserName = "no.reply.big.bangstore@gmail.com",  // replace with valid value
                        //Password = "29202000a"  // replace with valid value2
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }


        public ActionResult Sent()
        {
            return View();
        }
    }
}