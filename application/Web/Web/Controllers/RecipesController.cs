using Newtonsoft.Json.Linq;
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
    public class RecipesController : ApiController
    {
        private KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        // GET: api/Recipes
        public IQueryable<Recipe> GetRecipes()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            return db.RecipeSet;
        }

        // GET: api/Recipes/5
        [ResponseType(typeof(Recipe))]
        public IHttpActionResult GetRecipe(int id)
        {
            Recipe przepis = db.RecipeSet.Find(id);
            if (przepis == null)
            {
                return NotFound();
            }

            return Ok(przepis);
        }

        // PUT: api/Recipes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecipe([FromBody]JObject recipe)
        {
            String token = recipe["token"].ToObject<String>();
            Recipe przepis = recipe["recipe"].ToObject<Recipe>();

            var user = db.UserSet.Where(x => x.Token.Equals(token));

            if (!ModelState.IsValid || !user.Any())
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }

            db.Entry(przepis).State = EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/Recipes
        [ResponseType(typeof(Recipe))]
        public IHttpActionResult PostRecipe([FromBody]JObject recipe)
        {
            String token = recipe["token"].ToObject<String>();
            Recipe przepis = recipe["recipe"].ToObject<Recipe>();

            var user = db.UzytkownikSet.Where(x => x.token.Equals(token));

            if (!ModelState.IsValid || !user.Any())
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }

            db.RecipeSet.Add(przepis);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = przepis.Id }, przepis);
        }

        // DELETE: api/Recipes/5
        [ResponseType(typeof(Przepis))]
        public IHttpActionResult DeleteRecipe(int id, String token)
        {
            var user = db.UzytkownikSet.Where(x => x.token.Equals(token));

            if (!user.Any())
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }

            Recipe przepis = db.RecipeSet.Find(id);
            if (przepis == null)
            {
                return NotFound();
            }

            db.RecipeSet.Remove(przepis);
            db.SaveChanges();

            return Ok(przepis);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrzepisExists(int id)
        {
            return db.RecipeSet.Count(e => e.Id == id) > 0;
        }
    }
}