using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaClient.Objects
{
    public class CitaObject
    {
        public int id_cita { get; set; }

        public int id_paciente { get; set; }

        public int id_doctor { get; set; }

        public string fecha { get; set; }

        public int hora { get; set; }

        public int status { get; set; }

        public string observacion { get; set; }
        //
        public string doctor { get; set; }

        public DateTime fechag { get; set; }
        public TimeSpan horag { get; set; }
    }
}