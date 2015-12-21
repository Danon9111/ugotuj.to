using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers
{
    public class RandomRecipeController : ApiController
    {
        KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        public RecipeHelper Get()
        {
            var element = db.RecipeSet.OrderBy(x => x.Creation_Date).Take(db.UserSet.Count()-5).ToList();
            
            Shuffle(element);
            
            if (element.Count() == 0) return new RecipeHelper("Nie znaleziono żadnych przepisów!");

            var recipeFromDb = element.ToList()[0];

            RecipeHelper recipe = new RecipeHelper();
            recipe.id = recipeFromDb.Id;
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

            return recipe;
        }
        public void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = (rnd.Next(0, n) % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
