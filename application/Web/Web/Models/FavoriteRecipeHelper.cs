using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class FavoriteRecipeHelper
    {
        public int id { get; set; }
        public int recipeId { get; set; }
        public String token { get; set; }
    }
}