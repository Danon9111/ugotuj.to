using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers
{
    public class LogoutController : ApiController
    {
        KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        public String Get(String token)
        {
            var element = from usr in db.UzytkownikSet
                          where usr.token == token
                          select usr;

            if (element.Count() == 0) return "Incorrect token!";

            element.ToList()[0].token = null;

            db.SaveChanges();
            return "Logout";
        }

        public String Post([FromBody]String token)
        {
            var element = from usr in db.UzytkownikSet
                          where usr.token.Equals(token)
                          select usr;

            if (element.Count() == 0) return "Incorrect token!";

            element.ToList()[0].token = null;

            db.SaveChanges();
            return "Logout";
        }
    }
}
