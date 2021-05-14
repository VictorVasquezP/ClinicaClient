using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClinicaClient.Models;
using ClinicaClient.Objects;
using Newtonsoft.Json;

namespace ClinicaClient.Controllers
{
    public class AsistenteController : Controller
    {
        // GET: Asistente
        public async Task<ActionResult> Index()
        {
            Usuarios usuario = (Usuarios)Session["Usuario"];
            if (usuario == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            ViewBag.estadoMenuAsistente = "mm-active";

            ResultJson resultado = new ResultJson();
            var url = "http://proyectoclinica.somee.com/";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage resp = await client.GetAsync("/Asistente/Tickets");
                    if (resp.IsSuccessStatusCode)
                    {
                        var medResponde = resp.Content.ReadAsStringAsync().Result;
                        System.Diagnostics.Debug.WriteLine("Exito " + medResponde);
                        resultado = JsonConvert.DeserializeObject<ResultJson>(medResponde);
                        
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

            return View(resultado.tickets);
        }


        public async Task<ActionResult> Previsualizar(int id)
        {
            ResultJson resultado = new ResultJson();
            var url = "http://proyectoclinica.somee.com/";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage resp = await client.GetAsync($"/Asistente/Previsualizar?id={id}");
                    if (resp.IsSuccessStatusCode)
                    {
                        var medResponde = resp.Content.ReadAsStringAsync().Result;
                        System.Diagnostics.Debug.WriteLine("Exito " + medResponde);
                        resultado = JsonConvert.DeserializeObject<ResultJson>(medResponde);

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

            ViewData["doc"] = resultado.ticket.documento;
            return View();
        }

    }
}