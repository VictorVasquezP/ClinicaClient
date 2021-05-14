
using ClinicaClient.Models;
using ClinicaClient.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ClinicaClient.Controllers
{
    public class PacientesController : Controller
    {
        // GET: Pacientes
        public async Task<ActionResult> Index()
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

                    HttpResponseMessage resp = await client.GetAsync("/Pacientes/ListarPacientes");
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

            ViewBag.estadoPacientes = "mm-active";
            return View(resultado.pacientes);
        }

        [HttpPost]
        public async Task<ActionResult> Registrar([Bind(Include = "nombre,ape_pat,ape_mat,usuario,password")] Pacientes paciente)
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

                    HttpResponseMessage resp = await client.PostAsJsonAsync("/Pacientes/Registrar", new { paciente });
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

            if (resultado.result)
            {
                TempData["resultado"] = "Paciente eliminado con éxito";
            }
            else
            {
                TempData["resultado"] = "Paciente no eliminado";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Eliminar(int id)
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

                    HttpResponseMessage resp = await client.PostAsJsonAsync("/Pacientes/Eliminar", new { id });
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

            
            if (resultado.result)
            {
                TempData["resultado"] = "Paciente eliminado con éxito";
            }
            else
            {
                TempData["resultado"] = "Paciente no eliminado";
            }
            return RedirectToAction("Index");
        }
    }
}