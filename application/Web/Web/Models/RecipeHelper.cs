using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class RecipeHelper
    {
        public RecipeHelper(){ }
        public RecipeHelper(String error)
        {
            this.error = error;
        }

        public RecipeHelper(Recipe recipe)
        {
            this.id = recipe.Id;
            this.category = recipe.Category;
            this.description = recipe.Description;
            this.creationDate = recipe.Creation_Date.Value;
            this.difficulty = recipe.Dificult.Value;
            this.ingredients = recipe.Ingredients;
            this.name = recipe.Name;
            this.photo = recipe.Photo;
            this.preparation = recipe.Preparation;
            this.readyIn = recipe.Preparation_Time;
            this.user = recipe.User.Login;
            this.video = recipe.Video;
        }

        public int? id { get; set; }
        public String category { get; set; }
        public String description { get; set; }
        public DateTime creationDate { get; set; }
        public int? difficulty { get; set; }
        public String ingredients { get; set; }
        public String name { get; set; }
        public String photo { get; set; }
        public String preparation { get; set; }
        public int? readyIn { get; set; }
        public String user { get; set; }
        public String video { get; set; }
        public String error { get; set; }
        public String token { get; set; }
        public Boolean favorite { get; set; }
    }
}