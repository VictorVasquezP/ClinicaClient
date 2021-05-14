using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaClient.Objects
{
    public class Receta
    {
        public int id_receta { get; set; }

        public int id_cita { get; set; }

        public string documento { get; set; }

        public string ruta { get; set; }

        public string ids_medicamentos { get; set; }

        public string fecha { get; set; }

        public string observacion { get; set; }

        public string instruccion { get; set; }
    }
}