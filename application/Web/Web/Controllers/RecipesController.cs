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
        public IQueryable<Przepis> GetRecipes()
        {
            return db.PrzepisSet;
        }

        // GET: api/Recipes/5
        [ResponseType(typeof(Przepis))]
        public IHttpActionResult GetRecipe(int id)
        {
            Przepis przepis = db.PrzepisSet.Find(id);
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
            Przepis przepis = recipe["recipe"].ToObject<Przepis>();

            var user = db.UzytkownikSet.Where(x => x.token.Equals(token));

            if (!ModelState.IsValid || !user.Any())
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }

            db.Entry(przepis).State = EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/Recipes
        [ResponseType(typeof(Przepis))]
        public IHttpActionResult PostRecipe([FromBody]JObject recipe)
        {
            String token = recipe["token"].ToObject<String>();
            Przepis przepis = recipe["recipe"].ToObject<Przepis>();

            var user = db.UzytkownikSet.Where(x => x.token.Equals(token));

            if (!ModelState.IsValid || !user.Any())
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }

            db.PrzepisSet.Add(przepis);
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

            Przepis przepis = db.PrzepisSet.Find(id);
            if (przepis == null)
            {
                return NotFound();
            }

            db.PrzepisSet.Remove(przepis);
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
            return db.PrzepisSet.Count(e => e.Id == id) > 0;
        }
    }
}