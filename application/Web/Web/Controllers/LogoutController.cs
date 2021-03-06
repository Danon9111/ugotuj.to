﻿using System;
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
            var element = from usr in db.UserSet
                          where usr.Token.Equals(token)
                          select usr;

            if (element.Count() == 0) return "Nieprawidłowy token!";

            element.ToList()[0].Token = null;

            db.SaveChanges();
            return "Wylogowano!";
        }

        public String Post([FromBody]String token)
        {
            var element = from usr in db.UserSet
                          where usr.Token.Equals(token)
                          select usr;

            if (element.Count() == 0) return "Nieprawidłowy token!";

            element.ToList()[0].Token = null;

            db.SaveChanges();
            return "Wylogowano!";
        }
    }
}
