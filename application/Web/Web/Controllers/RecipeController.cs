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

        public Recipe Get(int id)
        {
            // Security check
            var element = from usr in db.PrzepisSet
                          where usr.Id == id
                          select usr;

            if (element.Count() == 0) return new Recipe("This recipe not exist!");

            var recipeFromDb = element.ToList()[0];

            Recipe recipe = new Recipe();
            recipe.category = recipeFromDb.kategoria;
            recipe.creationDate = recipeFromDb.data_utworzenia.Value;
            recipe.description = recipeFromDb.opis;
            recipe.difficulty = (int)recipeFromDb.trudnosc;
            recipe.ingredients = recipeFromDb.skladniki;
            recipe.name = recipeFromDb.nazwa;
            recipe.photo = recipeFromDb.zdjecie;
            recipe.readyIn = recipeFromDb.czas_wykonania;
            recipe.video = recipeFromDb.film;

            return recipe;
        }

        public class Recipe
        {
            public Recipe(){ }

            public Recipe(String error)
            {
                this.error = error;
            }

            public String name { get; set; }
            public String description { get; set; }
            public String preparation { get; set; }
            public String photo { get; set; }
            public String video { get; set; }
            public int readyIn { get; set; }
            public DateTime creationDate { get; set; }
            public int difficulty { get; set; }
            public String category { get; set; }
            public String ingredients { get; set; }
            public String error { get; set; }
        }
    }
}
