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
                          where usr.Token.Equals(recipe.token)
                          select usr;

            if (element.Count() == 0) return "Token nieprawidłowy!";
            if (recipe.name.Length == 0) return "Nazwa nie może być pusta!";
            if (recipe.description.Length == 0) return "Opis nie może być pusty!";
            if (recipe.category.Length == 0) return "Kategoria nie może być pusta!";
            if (recipe.preparation.Length == 0) return "Przepis musi zawierać jakikolwiek tekst!";
            if (recipe.ingredients.Length == 0) return "Przepis musi zawierać składniki!";
            if (recipe.difficulty == null) return "Nieprawidłowa trudność!";
            if (recipe.preparation.Length == 0) return "Nieprawidłowy czas wykonania!";

            Recipe przepis = new Recipe();
            przepis.Name = recipe.name;
            przepis.Description = recipe.description;
            przepis.Category = recipe.category;
            przepis.Preparation = recipe.preparation;
            przepis.Ingredients = recipe.ingredients;
            przepis.Dificult = recipe.difficulty;
            przepis.Photo = recipe.photo;
            przepis.Preparation_Time = recipe.readyIn.Value;
            przepis.Creation_Date = DateTime.Now;
            przepis.Video = recipe.video;
            przepis.UserId = element.First().Id;

            db.RecipeSet.Add(przepis);
            db.SaveChanges();

            return "Przepis został dodany!";
        }
    }
}
