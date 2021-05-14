namespace ClinicaClient.Models
{
    using System;

    public class Tickets
    {
        public int id_ticket { get; set; }

        public int id_cita { get; set; }

        public string documento { get; set; }

        public string ruta { get; set; }

        public DateTime fecha { get; set; }

        public double total { get; set; }
    }
}
