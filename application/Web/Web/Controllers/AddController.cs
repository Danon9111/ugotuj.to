using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers
{
    public class AddController : ApiController
    {
        // POST api/values
        public String Post([FromBody]Recipe recipe)
        {
            KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

            // Security check
            var element = from usr in db.UzytkownikSet
                          where usr.token == recipe.token
                          select usr;

            if (element.Count() == 0) return "Incorrect token!";

            Przepis przepis = new Przepis();
            przepis.nazwa = recipe.name;
            przepis.opis = recipe.description;
            przepis.kategoria = recipe.category;
            przepis.przygotowanie = recipe.preparation;
            przepis.skladniki = recipe.ingredients;
            przepis.trudnosc = recipe.difficulty;
            przepis.zdjecie = recipe.photo;
            przepis.czas_wykonania = recipe.readyIn;
            przepis.data_utworzenia = DateTime.Now;
            przepis.film = recipe.video;

            db.PrzepisSet.Add(przepis);

            db.SaveChanges();

            return "Recipe was added!";
        }

        public class Recipe
        {
            public String token { get; set; }
            public String name { get; set; }
            public String description { get; set; }
            public String preparation { get; set; }
            public String photo { get; set; }
            public String video { get; set; }
            public int readyIn { get; set; }
            public int difficulty { get; set; }
            public String category { get; set; }
            public String ingredients { get; set; }
        }
    }
}
