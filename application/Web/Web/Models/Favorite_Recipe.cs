//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Favorite_Recipe
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
    
        public virtual User User { private get; set; }
        public virtual Recipe Recipe { private get; set; }
    }
}
