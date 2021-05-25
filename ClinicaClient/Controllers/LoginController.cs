using ClinicaClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using Org.BouncyCastle.Ocsp;
using System.IO;
using Newtonsoft.Json;
using ClinicaClient.Objects;
using System.Web.WebPages;

namespace ClinicaClient.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : Controller
    {
       
        // GET: Login
        public async Task<ActionResult> Index()
        {
            return View();
        }

        //POST:Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(string usu, string pas)
        {
            if(usu.IsEmpty() && pas.IsEmpty())
            {
                ViewBag.Mensaje = "El Correo y/o contraseña son incorrecta";
                ViewBag.usua = usu;
                return View();
            }
            ResultJson resultado = new ResultJson();
            string hashPassword = EncodePassword(pas);
            string usuario = usu;
            string password = hashPassword;
            var url = "http://proyectoclinica.somee.com/";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage resp = await client.PostAsJsonAsync("/Login/LoginUsuario",new {usuario,password});
                    if (resp.IsSuccessStatusCode)
                    {
                        var medResponde = resp.Content.ReadAsStringAsync().Result;
                        resultado = JsonConvert.DeserializeObject<ResultJson>(medResponde);
                        System.Diagnostics.Debug.WriteLine("Exito " + medResponde);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("No respuesta");
                    }
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Error en la peticion");
            }
            
            if(resultado.usuarios != null)
            {
                resultado.usuarios.password = null;
                Session["Usuario"] = resultado.usuarios;
                Session.Timeout = 30;
                switch (resultado.usuarios.tipo)
                {
                    case 0:
                        return RedirectToAction("", "Home");
                    case 1:
                        return RedirectToAction("", "Asistente");
                    case 2:
                        return RedirectToAction("", "Admin");
                    default:
                        ViewBag.Mensaje = "El Correo y/o contraseña son incorrecta";
                        ViewBag.usua = usu;
                        return View();
                }
            }
            
            ViewBag.Mensaje = "El Correo y/o contraseña son incorrecta";
            ViewBag.usua = usu;
            return View();
        }

        public ActionResult LogOut()
        {
            Session["Usuario"] = null;
            Session.Abandon();
            return RedirectToAction("", "Login");
        }

        public static string EncodePassword(string originalPassword)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }

        /*

           var request = (HttpWebRequest)WebRequest.Create(url);
           request.Method = "GET";
           request.ContentType = "application/json";
           request.Accept = "application/json";

           try
           {
               using(WebResponse response = request.GetResponse())
               {

                   using(Stream strReader = response.GetResponseStream())
                   {
                       if(strReader == null)
                       {
                           System.Diagnostics.Debug.WriteLine("No exito");
                       }
                       else
                       {
                           using(StreamReader reader = new StreamReader(strReader))
                           {
                               string responseResult = reader.ReadToEnd();
                               System.Diagnostics.Debug.WriteLine("Result: "+responseResult);
                           }
                       }
                   }
               }

           }
           catch (Exception)
           {
               throw;
               System.Diagnostics.Debug.WriteLine("Error al hacer la peticion");
           }
           */
    }
}