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

        public String Post([FromBody]User user)
        {
            // e-mail address, login, password, confirm password
            String token = user.token;
            String oldPassword = user.oldPassword;
            String newPassword = user.newPassword;

            // Security check
            var element = from usr in db.UzytkownikSet
                          where usr.token == token && usr.haslo == oldPassword
                          select usr;

            if (element.Count() == 0) return "Incorrect token or old password!";
            if (newPassword.Length < 5) return "Incorect new password!";

            var userAccount = element.ToList()[0];
            userAccount.haslo = newPassword;
            db.SaveChanges();

            return "Password changed!";
        }

        public class User
        {
            public String token { get; set; }
            public String oldPassword { get; set; }
            public String newPassword { get; set; }
        }

    }
}
