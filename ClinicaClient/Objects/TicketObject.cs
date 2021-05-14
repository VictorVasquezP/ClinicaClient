using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaClient.Objects
{
    public class TicketObject
    {
        public int id_ticket { get; set; }
        public string nombreDoctor { get; set; }
        public string usuarioDoctor { get; set; }
        public string nombrePaciente { get; set; }
        public string fecha { get; set; }
        public double total { get; set; }
        public int id_cita { get; set; }
        public string documento { get; set; }
        public string ruta { get; set; }
    }
}