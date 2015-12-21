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
    public class RemoveFavoriteRecipeController : ApiController
    {
        private KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        [HttpPost]
        public Boolean PostFavorite_Recipe(FavoriteRecipeHelper request)
        {
            var user = db.UserSet.Where(x => x.Token.Equals(request.token));
            if (!user.Any()) return false;

            // jeżeli nie ma takiego przepisu
            var recipe = db.RecipeSet.Where(x => x.Id == request.recipeId);
            if (!recipe.Any()) return false;

            var userId = user.First().Id;

            var favoriteRecipe = db.Favorite_RecipeSet.Where(x => x.RecipeId == request.recipeId)
                .Where(y => y.UserId == userId);

            if (!favoriteRecipe.Any()) return false;

            db.Favorite_RecipeSet.Remove(favoriteRecipe.First());
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