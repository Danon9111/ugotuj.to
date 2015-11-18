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

        public Recipe Get()
        {
            var element = db.PrzepisSet.ToList();
            
            Shuffle(element);
            
            if (element.Count() == 0) return new Recipe("No recipes found!");

            var recipeFromDb = element.ToList()[0];

            Recipe recipe = new Recipe();
            recipe.id = recipeFromDb.Id;
            recipe.category = recipeFromDb.kategoria;
            recipe.creationDate = recipeFromDb.data_utworzenia.Value;
            recipe.description = recipeFromDb.opis;
            recipe.difficulty = (int)recipeFromDb.trudnosc;
            recipe.preparation = recipeFromDb.przygotowanie;
            recipe.ingredients = recipeFromDb.skladniki;
            recipe.name = recipeFromDb.nazwa;
            recipe.photo = recipeFromDb.zdjecie;
            recipe.readyIn = recipeFromDb.czas_wykonania;
            recipe.video = recipeFromDb.film;

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
