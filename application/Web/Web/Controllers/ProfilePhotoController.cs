using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers
{
    public class ProfilePhotoController : ApiController
    {
        private KsiazkaKucharskaModelContainer db = new KsiazkaKucharskaModelContainer();

        [HttpGet]
        public String GetPhotoPath(int id)
        { 
            var photo = db.User_PhotoSet.Where(x=>x.User.Id == id).ToList();
            if (!photo.Any()) return null;

            return photo.Last().Path;
        }

        [HttpGet]
        public String GetPhotoPath(String token)
        {
            var photo = db.User_PhotoSet.Where(x => x.User.Token.Equals(token)).ToList();
            if (!photo.Any()) return null;

            return photo.Last().Path;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> PostFormData(String token)
        {
            var users = db.UserSet.Where(x => x.Token.Equals(token));
            if (!users.Any()) Request.CreateResponse(HttpStatusCode.Unauthorized);
            var user = users.First();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            try
            {
                string root = HttpContext.Current.Server.MapPath("~/App_Data");
                var provider = new MultipartFormDataStreamProvider(root);

                // Read the form data and return an async task.
                await Request.Content.ReadAsMultipartAsync(provider);

                Random rand = new Random();
                MultipartFileData file = provider.FileData[0];
                var fileName = rand.Next(0,9999999).ToString() + file.Headers.ContentDisposition.FileName.Replace(@"\","").Replace("\"","");
                var sourceFile = file.LocalFileName;
                var targetPath = HttpContext.Current.Server.MapPath("~/img/profile/");
                string destFile = System.IO.Path.Combine(targetPath, fileName);
                if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }
                System.IO.File.Copy(sourceFile, destFile, true);
                
                User_Photo photo = new User_Photo()
                {
                    Path = "/img/profile/" + fileName,
                    User = user
                };
                db.User_PhotoSet.Add(photo);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
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
