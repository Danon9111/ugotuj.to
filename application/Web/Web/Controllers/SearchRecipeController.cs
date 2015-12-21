using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers
{
    public class SearchRecipeController : ApiController
    {
        KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        public List<RecipeHelper> Get(String query)
        {
            var recipes = from recipe in db.RecipeSet
                          where recipe.Name.Contains(query)
                          select recipe;

            if (recipes.Count() == 0) return null;

            recipes = recipes.Take(5);

            List<RecipeHelper> recipeList = new List<RecipeHelper>();

            foreach (var element in recipes)
            {
                RecipeHelper recipe = new RecipeHelper();
                recipe.id = element.Id;
                recipe.category = element.Category;
                recipe.creationDate = element.Creation_Date.Value;
                recipe.description = element.Description;
                recipe.difficulty = (int)element.Dificult;
                recipe.preparation = element.Preparation;
                recipe.ingredients = element.Ingredients;
                recipe.name = element.Name;
                recipe.photo = element.Photo;
                recipe.readyIn = element.Preparation_Time;
                recipe.video = element.Video;
                recipeList.Add(recipe);
            }
            
            return recipeList;
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
