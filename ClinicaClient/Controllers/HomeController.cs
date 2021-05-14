using ClinicaClient.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClinicaClient.Models;

namespace ClinicaClient.Controllers
{
    public class HomeController : Controller
    {  
        public ActionResult Index()
        {
            Usuarios usuario = (Usuarios)Session["Usuario"];
            if (usuario == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            ViewBag.estadoMenu = "mm-active";
            return View();
        }

    }
}