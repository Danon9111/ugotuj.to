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
        public String Post([FromBody]RecipeHelper recipe)
        {
            KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

            // Security check
            var element = from usr in db.UserSet
                          where usr.Token == recipe.token
                          select usr;

            if (element.Count() == 0) return "Token nieprawidłowy!";

            Recipe przepis = new Recipe();
            przepis.Name = recipe.name;
            przepis.Description = recipe.description;
            przepis.Category = recipe.category;
            przepis.Preparation = recipe.preparation;
            przepis.Ingredients = recipe.ingredients;
            przepis.Dificult = recipe.difficulty;
            przepis.Photo = recipe.photo;
            przepis.Preparation_Time = recipe.readyIn;
            przepis.Creation_Date = DateTime.Now;
            przepis.Video = recipe.video;
            przepis.UserId = element.First().Id;

            if (przepis.Name.Length == 0) return "Nazwa nie może być pusta!";
            if (przepis.Description.Length == 0) return "Opis nie może być pusty!";
            if (przepis.Category.Length == 0) return "Kategoria nie może być pusta!";
            if (przepis.Preparation.Length == 0) return "Przepis musi zawierać jakikolwiek tekst!";
            if (przepis.Ingredients.Length == 0) return "Przepis musi zawierać składniki!";
            if (przepis.Dificult == null) return "Nieprawidłowa trudność!";
            if (przepis.Preparation_Time <= 0) return "Nieprawidłowy czas wykonania!";

            db.RecipeSet.Add(przepis);

            db.SaveChanges();

            return "Przepis został dodany!";
        }
    }
}
