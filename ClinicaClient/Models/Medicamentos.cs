namespace ClinicaClient.Models
{
    using System;

    public class Medicamentos
    {
        public int id_medicamento { get; set; }

        public string nombre { get; set; }

        public string dosis { get; set; }

        public double precio { get; set; }

        public string indicaciones { get; set; }
    }
}
