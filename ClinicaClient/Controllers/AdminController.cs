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
    public class AdminController : Controller
    {
        // GET: Admin
        public async Task<ActionResult> Index()
        {
            Usuarios usuario = (Usuarios)Session["Usuario"];
            if (usuario == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            ResultJson resultado = new ResultJson();
            var url = "http://proyectoclinica.somee.com/";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage resp = await client.GetAsync("/Usuarios/ListarUsuarios");
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
            return View(resultado.listaUsuarios);
        }



        [HttpPost]
        public async Task<ActionResult> Registrar([Bind(Include = "nombre,ape_pat,ape_mat,usuario,password,tipo")] Usuarios usuarios)
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

                    HttpResponseMessage resp = await client.PostAsJsonAsync("/Usuarios/Registrar", new { usuarios });
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
                TempData["resultado"] = "Usuario registrado con éxito";
            }
            else
            {
                TempData["resultado"] = "Usuario no registrado";
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

                    HttpResponseMessage resp = await client.PostAsJsonAsync("/Usuarios/Eliminar", new { id });
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
                TempData["resultado"] = "Usuario eliminado con éxito";
            }
            else
            {
                TempData["resultado"] = "Usuario no eliminado";
            }
            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<ActionResult> Editar([Bind(Include = "id_usuario,nombre,ape_pat,ape_mat,usuario,password,tipo")] Usuarios usuarios)
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

                    HttpResponseMessage resp = await client.PostAsJsonAsync("/Usuarios/Editar", new { usuarios });
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
                TempData["resultado"] = "Usuario actualizaco con éxito";
            }
            else
            {
                TempData["resultado"] = "Usuario no actualizado";
            }
            return RedirectToAction("Index");
        }




    }
}