using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers
{
    public class ChangePasswordController : ApiController
    {
        KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        public String Post([FromBody]UserHelper user)
        {
            // e-mail address, login, password, confirm password
            String token = user.token;
            String oldPassword = user.oldPassword;
            String newPassword = user.newPassword;

            // Security check
            var element = from usr in db.UserSet
                          where usr.Token.Equals(token) && usr.Password.Equals(oldPassword)
                          select usr;

            if (element.Count() == 0) return "Nieprawidłowy token!";
            if (newPassword.Length < 5) return "Nowe hasło jest nieprawidłowe!";

            var userAccount = element.ToList()[0];
            userAccount.Password = newPassword;
            db.SaveChanges();

            return "Hasło zostało zmienione!";
        }
    }
}
