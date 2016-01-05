using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class FavoriteRecipeHelper
    {
        public FavoriteRecipeHelper() { }

        public FavoriteRecipeHelper(List<Favorite_Recipe> favoriteRecipes)
        {
            foreach (var element in favoriteRecipes)
            {
                recipes.Add(new RecipeHelper(element.Recipe));
            }
        }

        public int? id { get; set; }
        public int? recipeId { get; set; }
        public String token { get; set; }
        public List<RecipeHelper> recipes = new List<RecipeHelper>();
    }
}