using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers
{
    public class RecipeController : ApiController
    {
        KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        public RecipeHelper Get(int id)
        {
            var element = from usr in db.RecipeSet
                          where usr.Id == id
                          select usr;

            if (element.Count() == 0) return new RecipeHelper("Taki przepis nie istnieje!");

            var recipeFromDb = element.ToList()[0];

            RecipeHelper recipe = new RecipeHelper();
            recipe.category = recipeFromDb.Category;
            recipe.creationDate = recipeFromDb.Creation_Date.Value;
            recipe.description = recipeFromDb.Description;
            recipe.difficulty = (int)recipeFromDb.Dificult;
            recipe.preparation = recipeFromDb.Preparation;
            recipe.ingredients = recipeFromDb.Ingredients;
            recipe.name = recipeFromDb.Name;
            recipe.photo = recipeFromDb.Photo;
            recipe.readyIn = recipeFromDb.Preparation_Time;
            recipe.video = recipeFromDb.Video;
            recipe.user = recipeFromDb.User.Login;

            return recipe;
        }
    }
}
