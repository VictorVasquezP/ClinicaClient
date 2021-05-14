using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaClient.Objects
{
    public class Medicamento
    {
        public int id { set; get; }
        public string nombre { set; get; }
        public string dosis { set; get; }
        public double precio { set; get; }
        public string indicaciones { set; get; }

        public override string ToString()
        {
            return String.Format("Id: {0}, nombre: {1}, dosis: {2}, precio: {3}", id, nombre, dosis, precio);
        }
    }
}