using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaClient.Objects
{
    public class Paciente
    {
        public int id_paciente { set; get; }
        public string nombre { set; get; }
        public string ape_pat { set; get; }
        public string ape_mat { set; get; }
        public string usuario { set; get; }
        public string password { set; get; }

        public Paciente()
        {

        }
        public Paciente(string nombre, string ape_pat, string ape_mat, string usuario)
        {
            this.nombre = nombre;
            this.ape_pat = ape_pat;
            this.ape_mat = ape_mat;
            this.usuario = usuario;
        }
    }
}