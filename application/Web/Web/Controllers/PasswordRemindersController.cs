using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Http.Description;
using Web.Models;

namespace Web.Controllers
{
    public class PasswordRemindersController : ApiController
    {
        private KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        // GET: api/PasswordReminders
        public bool GetNewPassword(String email)
        {
            var element = db.UserSet.Where(x => x.Email.Equals(email)).ToList();
            if (element.Any())
            {
                var user = element.ElementAt(0);
                if (user.PasswordReminder.Any())
                {
                    db.PasswordReminderSet.Remove(user.PasswordReminder.First());
                }

                String hash = GenRandString(20);
                user.PasswordReminder.Add(new PasswordReminder()
                {
                    hash_string = hash,
                    creation_date = DateTime.Now.ToString()
                });

                SendMail(user.Email, hash);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public String GetResetPassword(String hash)
        {
            var element = db.PasswordReminderSet.Where(x => x.hash_string.Equals(hash)).ToList();
            if (element.Any())
            {
                // Wysyłanie maila z nowym hasłem
                String password = GenRandString(8);
                element.ElementAt(0).User.Password = password;
                db.SaveChanges();
                return password;
            }
            return null;
        }

        public String GenRandString(int length)
        {
            byte[] randBuffer = new byte[length];
            RandomNumberGenerator.Create().GetBytes(randBuffer);
            return System.Convert.ToBase64String(randBuffer).Remove(length).Replace('+','a');
        }

        public void SendMail(String to, String hash)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.webio.pl", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential("noreply@ugotuj.to.hostingasp.pl", "XXXXX!");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("noreply@ugotuj.to.hostingasp.pl", "ugotuj.to");
            mail.Subject = "Przypomnienie hasła w serwisie Ugotuj.to";
            mail.To.Add(new MailAddress(to));
            mail.Body = "Testowa wiadomość: " + hash;

            smtpClient.Send(mail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}