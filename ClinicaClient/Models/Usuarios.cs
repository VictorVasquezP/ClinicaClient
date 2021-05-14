namespace ClinicaClient.Models
{
    using System;

    public  class Usuarios
    {
        public int id_usuario { get; set; }

        public string nombre { get; set; }

        public string ape_pat { get; set; }

        public string ape_mat { get; set; }

        public string usuario { get; set; }

        public string password { get; set; }

        public int tipo { get; set; }
    }
}
