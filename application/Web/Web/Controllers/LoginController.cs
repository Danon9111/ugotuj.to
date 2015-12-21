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
        public UserHelper Get(String login, String password)
        {
            // Security check
            var element = from usr in db.UserSet
                          where usr.Login.Equals(login) && usr.Password.Equals(password)
                          select usr;

            if (element.Count() == 0) return new UserHelper("Nieprawidłowy login lub hasło!");

            UserHelper response = new UserHelper();
            response.token = GenRandString(64);
            element.ToList()[0].Token = response.token;

            db.SaveChanges();

            return response;
        }

        // POST api/values
        public UserHelper Post([FromBody]UserHelper user)
        {
            // e-mail address, login, password, confirm password
            String login = user.login;
            String password = user.password;

            // Security check
            var element = from usr in db.UserSet
                          where usr.Login.Equals(login) && usr.Password.Equals(password)
                          select usr;

            if (element.Count() == 0) return new UserHelper("Nieprawidłowy login lub hasło!");
            
            UserHelper response = new UserHelper();
            response.token = GenRandString(64);
            element.ToList()[0].Token = response.token;

            db.SaveChanges();

            return response;
        }

        public string GenRandString(int length)
        {
            byte[] randBuffer = new byte[length];
            RandomNumberGenerator.Create().GetBytes(randBuffer);
            return System.Convert.ToBase64String(randBuffer).Remove(length).Replace('+','F');
        }
    }
}
