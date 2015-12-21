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
    public class EditController : ApiController
    {
        private KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        public String PostPrzepis(RecipeHelper przepis)
        {
            var user = db.UserSet.Where(x => x.Token.Equals(przepis.token));
            if (!user.Any()) return "Niepoprawny token!";
            var recipe = user.First().Recipe.Where(x => x.Id == przepis.id);
            if (!recipe.Any()) return "Próbujesz edytować przepis, który nie należy do Ciebie!";
            var recipeToEdit = recipe.First();

            recipeToEdit.Preparation_Time = przepis.readyIn;
            recipeToEdit.Creation_Date = DateTime.Now;
            recipeToEdit.Video = przepis.video;
            recipeToEdit.Category = przepis.category;
            recipeToEdit.Name = przepis.name;
            recipeToEdit.Description = przepis.description;
            recipeToEdit.Preparation = przepis.preparation;
            recipeToEdit.Ingredients = przepis.ingredients;
            recipeToEdit.Dificult = przepis.difficulty;
            recipeToEdit.Photo = przepis.photo;

            db.SaveChanges();
            return "Przepis został zmieniony!";
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