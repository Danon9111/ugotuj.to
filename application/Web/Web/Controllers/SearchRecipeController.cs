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

        public List<Recipe> Get(String query)
        {
            var recipes = from recipe in db.PrzepisSet
                          where recipe.nazwa.Contains(query)
                          select recipe;

            if (recipes.Count() == 0) return null;

            recipes = recipes.Take(10);

            List<Recipe> recipeList = new List<Recipe>();

            foreach (var element in recipes)
            {
                Recipe recipe = new Recipe();
                recipe.id = element.Id;
                recipe.category = element.kategoria;
                recipe.creationDate = element.data_utworzenia.Value;
                recipe.description = element.opis;
                recipe.difficulty = (int)element.trudnosc;
                recipe.preparation = element.przygotowanie;
                recipe.ingredients = element.skladniki;
                recipe.name = element.nazwa;
                recipe.photo = element.zdjecie;
                recipe.readyIn = element.czas_wykonania;
                recipe.video = element.film;
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
