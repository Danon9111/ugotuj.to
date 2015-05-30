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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        public class User
        {
            public String login { get; set; }
            public String password { get; set; }
            public String email { get; set; }
        }

        // POST api/values
        public String Post([FromBody]User user)
        {
            // e-mail address, login, password, confirm password
            String login = user.login;
            String password = user.password;
            String email = user.email;

            // Security check
            var element = from usr in db.UzytkownikSet
                          where usr.login == login
                          select usr;

            if (element.Count() != 0) return "Użytkownik o takim loginie istnieje!";
            if (password.Length < 5) return "Zbyt krótkie hasło!";
            if (!IsValid(email)) return "Podane hasło jest niepoprawne!";

            Uzytkownik uzytkownik = new Uzytkownik();
            uzytkownik.login = login;
            uzytkownik.nazwa = login;
            uzytkownik.haslo = password;
            uzytkownik.email = email;
            
            db.UzytkownikSet.Add(uzytkownik);

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

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
