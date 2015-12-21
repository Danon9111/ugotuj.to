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
    public class RemoveController : ApiController
    {
        private KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();
        
        public String PostPrzepis(RecipeHelper przepis)
        {
            var user = db.UserSet.Where(x => x.Token.Equals(przepis.token));
            if (!user.Any()) return "Niepoprawny token!";
            var recipe = user.First().Recipe.Where(x => x.Id == przepis.id);
            if (!recipe.Any()) return "Próbujesz usunąć przepis, który nie należy do Ciebie!";

            db.RecipeSet.Remove(recipe.First());
            db.SaveChanges();
            return "Przepis został usunięty!";
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