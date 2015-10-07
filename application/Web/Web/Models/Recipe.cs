using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Recipe
    {
        public Recipe() { }

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