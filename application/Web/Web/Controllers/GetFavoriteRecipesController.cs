using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Models;
namespace Web.Controllers
{
    public class GetFavoriteRecipesController : ApiController
    {
        private KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        [HttpPost]
        public FavoriteRecipeHelper GetFavorite_RecipeSet(FavoriteRecipeHelper request)
        {
            var selectedUser = db.UserSet.Where(x => x.Token.Equals(request.token));

            if (!selectedUser.Any()) return null;

            var userId = selectedUser.First().Id;

            var favoriteRecipes = db.Favorite_RecipeSet.Include(x=>x.Recipe).Where(y => y.UserId == userId).ToList();
            FavoriteRecipeHelper favoriteRecipeHelper = new FavoriteRecipeHelper(favoriteRecipes);

            return favoriteRecipeHelper;
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
