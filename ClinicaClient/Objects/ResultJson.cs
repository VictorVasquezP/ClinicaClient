using ClinicaClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicaClient.Objects
{
    public class ResultJson
    {
        /*LoginController*/
        public Usuarios usuarios { get; set; }
        public bool result { get; set; }

        /*CitasController*/
        public List<CitasDoctorObject> listaCitasPendientes { get; set; }
        public List<CitasDoctorObject> listaCitasAceptadas { get; set; }
        public List<CitasDoctorObject> listaCitasRechazadas { get; set; }
        public List<CitasDoctorObject> listaCitasRealizadas { get; set; }
        public List<Pacientes> listaPacientes { get; set; }
        public CitaDetallesObject citaDetalles { get; set; }

        /*AsistenteController*/
        public List<TicketObject> tickets { get; set; }
        public Tickets ticket { get; set; }

        /*PacientesController*/
        public List<Pacientes> pacientes { get; set; }

        /*RecetasController*/
        public List<Recetas> recetas { get; set; }
        public Recetas receta { get; set; }
    }
}
 