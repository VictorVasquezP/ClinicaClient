namespace ClinicaClient.Models
{
    using System;


    public  class Pacientes
    {
        public int id_paciente { get; set; }

        public string nombre { get; set; }

        public string ape_pat { get; set; }

        public string ape_mat { get; set; }

        public string usuario { get; set; }

        public string password { get; set; }
    }
}
