using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class UserHelper
    {
        public List<RecipeHelper> recipes;

        public UserHelper() { }
        public UserHelper(String error)
        {
            this.error = error;
        }
        public String login { get; set; }
        public String password { get; set; }
        public String token { get; set; }
        public String oldPassword { get; set; }
        public String newPassword { get; set; }
        public String error { get; set; }
        public String email { get; set; }
    }
}