namespace ClinicaClient.Models
{
    using System;

    public class Recetas
    {
        public int id_receta { get; set; }

        public int id_cita { get; set; }

        public string documento { get; set; }

        public string ruta { get; set; }

        public string ids_medicamentos { get; set; }

        public DateTime fecha { get; set; }

        public string observacion { get; set; }

        public string instruccion { get; set; }
    }
}
