using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    class Persona
    {
        public string id { get; set; }
        public string nombre { get; set; }
    }

    public class HomeController : Controller
    {
        Uri uri = new Uri(ConfigurationManager.AppSettings["Uri"]);
        HttpClient client = new HttpClient()
        {
            DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["Token"]) }
        };
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        // GET: Home/Get
        public ActionResult Get()
        {
            try
            {
                var response = GetHttpClient.GetRequest<Persona>(client, uri, "values");
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }
        // GET: Home/GetId
        public ActionResult GetId(string id)
        {
            try
            {
                var response = GetHttpClient.GetRequest<Persona>(client, uri, "values", id);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
        // GET: Home/Post
        public ActionResult Post()
        {
            var persona = new Persona { id = "0", nombre = "nombre" };
            try
            {
                var response = GetHttpClient.PostRequest<Persona>(client, uri, "values", persona);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
        // GET: Home/Put
        public ActionResult Put(string id)
        {
            var persona = new Persona { id = id, nombre = "nombre" + id };
            try
            {
                var response = GetHttpClient.PutRequest<Persona>(client, uri, "values", persona, id);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
        // GET: Home/Delete
        public ActionResult Delete(string id)
        {
            var persona = new Persona { id = id, nombre = "nombre" + id };
            try
            {
                var response = GetHttpClient.PostRequest<Persona>(client, uri, "values", persona);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
    }
}