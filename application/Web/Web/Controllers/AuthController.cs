using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Web.Models;

namespace Web.Controllers
{
    public class AuthController : ApiController
    {
        private KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        [HttpPost]
        public Boolean Post(HttpRequestMessage token)
        {
            var txt = token.Content.ReadAsStringAsync().Result;

            var users = from element in db.UzytkownikSet
                        where element.token.Equals(txt)
                        select element;

            if (users.Count() == 1) return true;
            return false;
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