﻿using System;
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
    public class IsFavoriteRecipeController : ApiController
    {
        private KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        [HttpPost]
        public Boolean GetFavorite_RecipeSet(FavoriteRecipeHelper request)
        {
            var selectedUser = db.UserSet.Where(x => x.Token.Equals(request.token));

            if (!selectedUser.Any()) return false;

            var favoriteRecipe = selectedUser.First().Favorite_Recipe.Where(x => x.RecipeId == request.recipeId);

            return favoriteRecipe.Any();
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