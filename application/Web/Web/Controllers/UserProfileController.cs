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
    public class UserProfileController : ApiController
    {
        private KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        public UserHelper PostUzytkownik(UserHelper uzytkownik)
        {
            var users = db.UserSet.Where(x => x.Token.Equals(uzytkownik.token));

            if (!users.Any()) return null;
            var user = users.First();

            List<RecipeHelper> recipes = new List<RecipeHelper>();

            foreach (var recipe in user.Recipe)
            {
                recipes.Add(new RecipeHelper()
                {
                    category = recipe.Category,
                    creationDate = recipe.Creation_Date.Value,
                    video = recipe.Video,
                    description = recipe.Description,
                    difficulty = recipe.Dificult.Value,
                    id = recipe.Id,
                    ingredients = recipe.Ingredients,
                    name = recipe.Name,
                    photo = recipe.Photo,
                    preparation = recipe.Preparation,
                    readyIn = recipe.Preparation_Time
                });
            }

            UserHelper userToReturn = new UserHelper()
            {
                email = user.Email,
                login = user.Login,
                recipes = recipes
            };

            return userToReturn;
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