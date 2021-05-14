namespace ClinicaClient.Models
{
    using System;

    public  class Citas
    {
        public int id_cita { get; set; }

        public int id_paciente { get; set; }

        public int id_doctor { get; set; }

    
        public DateTime fecha { get; set; }

        public TimeSpan hora { get; set; }

        public int status { get; set; }

        public string observacion { get; set; }
    }
}
