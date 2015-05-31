using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers
{
    public class LoginController : ApiController
    {
        KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        // GET api/values
        public Response Get(String login, String password)
        {
            // Security check
            var element = from usr in db.UzytkownikSet
                          where usr.login == login && usr.haslo == password
                          select usr;

            if (element.Count() == 0) return new Response("Incorrect username or password!");

            db.UzytkownikSet.Remove(element.ToList()[0]);

            Response response = new Response();
            response.token = GenRandString(64);
            element.ToList()[0].token = response.token;

            db.UzytkownikSet.Add(element.ToList()[0]);
            db.SaveChanges();

            return response;
        }

        // POST api/values
        public Response Post([FromBody]User user)
        {
            // e-mail address, login, password, confirm password
            String login = user.login;
            String password = user.password;

            // Security check
            var element = from usr in db.UzytkownikSet
                          where usr.login == login && usr.haslo == password
                          select usr;

            if (element.Count() == 0) return new Response("Incorrect username or password!");

            db.UzytkownikSet.Remove(element.ToList()[0]);

            Response response = new Response();
            response.token = GenRandString(64);
            element.ToList()[0].token = response.token;

            db.UzytkownikSet.Add(element.ToList()[0]);
            db.SaveChanges();

            return response;
        }

        public string GenRandString(int length)
        {
            byte[] randBuffer = new byte[length];
            RandomNumberGenerator.Create().GetBytes(randBuffer);
            return System.Convert.ToBase64String(randBuffer).Remove(length);
        }

        public class User
        {
            public String login { get; set; }
            public String password { get; set; }
        }

        public class Response
        {
            public Response()
            {
            }
            public Response(String error)
            {
                this.error = error;
            }

            public String error { get; set; }
            public String token { get; set; }
        }
    }
}
