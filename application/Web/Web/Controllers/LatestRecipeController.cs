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
    public class LatestRecipeController : ApiController
    {
        private KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        // GET: api/LatestRecipe
        public List<RecipeHelper> GetPrzepisSet()
        {
            var recipes = db.RecipeSet.OrderByDescending(x => x.Creation_Date).Take(5);
            List<RecipeHelper> recipeList = new List<RecipeHelper>();

            foreach (var recipe in recipes)
            {
                var recipeToList =  new RecipeHelper
                {
                    category = recipe.Category,
                    description = recipe.Description,
                    creationDate = recipe.Creation_Date.Value,
                    difficulty = recipe.Dificult.Value,
                    id = recipe.Id,
                    ingredients = recipe.Ingredients,
                    name = recipe.Name,
                    photo = recipe.Photo,
                    preparation = recipe.Preparation,
                    readyIn = recipe.Preparation_Time,
                    user = recipe.User.Login,
                    video = recipe.Video
                };

                recipeList.Add(recipeToList);
            }

            return recipeList;
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