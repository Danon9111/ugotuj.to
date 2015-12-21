using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers
{
    public class RegistrationController : ApiController
    {
        KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        // GET api/values
        public UserHelper Get()
        {
            UserHelper user = new UserHelper();
            user.login = "test_login";
            user.password = "test_password";
            user.email = "test_email";
            return user;
        }
        
        // POST api/values
        public String Post([FromBody]UserHelper user)
        {
            // e-mail address, login, password, confirm password
            String login = user.login;
            String password = user.password;
            String email = user.email;

            // Security check
            var element = from usr in db.UserSet
                          where usr.Login == login
                          select usr;

            var element2 = from usr in db.UserSet
                          where usr.Email == email
                          select usr;

            if (element.Count() != 0) return "Użytkownik o takim loginie istnieje!";
            if (element2.Count() != 0) return "Istnieje użytkownik o takim adresie email!";
            if (password.Length < 5) return "Zbyt krótkie hasło!";
            if (!IsValid(email)) return "Podane hasło jest niepoprawne!";

            User uzytkownik = new Models.User();
            uzytkownik.Login = login;
            uzytkownik.Name = login;
            uzytkownik.Password = password;
            uzytkownik.Email = email;
            
            db.UserSet.Add(uzytkownik);

            db.SaveChanges();

            return "Konto użytkownika zostało dodane poprawnie!";
        }

        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
