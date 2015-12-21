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
    public class AddFavoriteRecipeController : ApiController
    {
        private KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();
        
        [HttpPost]
        public Boolean PostFavorite_Recipe(FavoriteRecipeHelper request)
        {
            var user = db.UserSet.Where(x => x.Token.Equals(request.token));
            if (!user.Any()) return false;

            // jeżeli jest już ulubiony dla tego użytkownika, to false
            var userFavorite = db.Favorite_RecipeSet.Where(x => x.RecipeId == request.recipeId);
            if (userFavorite.Any()) return false;

            // jeżeli nie ma takiego przepisu
            var recipe = db.RecipeSet.Where(x=>x.Id == request.recipeId);
            if (!recipe.Any()) return false;

            db.Favorite_RecipeSet.Add(new Favorite_Recipe()
            {
                RecipeId = request.recipeId,
                UserId = user.First().Id
            });
            
            db.SaveChanges();

            return true;
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