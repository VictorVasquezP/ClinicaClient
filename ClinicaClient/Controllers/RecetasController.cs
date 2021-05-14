
using ClinicaClient.Models;
using ClinicaClient.Objects;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ClinicaClient.Controllers
{
    public class RecetasController : Controller
    {
        
        // GET: Recetas
        public async Task<ActionResult> Index(int id)
        {
            ResultJson resultado = new ResultJson();
            var url = "http://proyectoclinica.somee.com/";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage resp = await client.GetAsync($"/Recetas/ListarRecetasPacientes?id={id}");
                    if (resp.IsSuccessStatusCode)
                    {
                        var medResponde = resp.Content.ReadAsStringAsync().Result;
                        resultado = JsonConvert.DeserializeObject<ResultJson>(medResponde);
                        System.Diagnostics.Debug.WriteLine("Exito " + medResponde);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("No respuesta");
                    }
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Error en la peticion");
            }

            
            return View(resultado.recetas);
        }

        public async Task<ActionResult> Previsualizar(int id)
        {
            ResultJson resultado = new ResultJson();
            var url = "http://proyectoclinica.somee.com/";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage resp = await client.GetAsync($"/Recetas/Previsualizar?id={id}");
                    if (resp.IsSuccessStatusCode)
                    {
                        var medResponde = resp.Content.ReadAsStringAsync().Result;
                        resultado = JsonConvert.DeserializeObject<ResultJson>(medResponde);
                        System.Diagnostics.Debug.WriteLine("Exito " + medResponde);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("No respuesta");
                    }
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Error en la peticion");
            }

            ViewData["doc"] = resultado.receta.documento;
            return View();
        }


        public void crearReceta()
        {
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            string path = Server.MapPath("~/Files/Recetas/");
            System.Diagnostics.Debug.WriteLine("path: "+path);
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(path+"prueba.pdf", FileMode.Create));

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("CLINICA MITCHELL");
            doc.AddCreator("Dr. Erick Echeverría");

            // Abrimos el archivo
            doc.Open();

            string pathImagen = Server.MapPath("~/Imagenes/");
            iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(pathImagen + "logo.png");
            //image1.ScalePercent(50f);
            float percentage = 0.0f;
            percentage = 150 / image1.Width;
            image1.ScalePercent(percentage * 50);
            //image1.Alignment = Element.ALIGN_LEFT;
            //image1.Top= 1;
            image1.Left = 0;
            doc.Add(image1);

            iTextSharp.text.Image imageFondo = iTextSharp.text.Image.GetInstance(pathImagen + "logoSalleFondo.png");
            imageFondo.Alignment = iTextSharp.text.Image.UNDERLYING;
            float percentage2 = 150 / imageFondo.Width;
            imageFondo.ScalePercent(percentage2 * 190);
            imageFondo.SetAbsolutePosition(170,370);
            doc.Add(imageFondo);


            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Escribimos el encabezamiento en el documento
            iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN,14, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
            iTextSharp.text.Font fontSub = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN,11, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
            Paragraph medico = new Paragraph("Dr. Erick Mitchell Echeverría Celaya",fontTitle);
            Paragraph clinica = new Paragraph("Clinica \"Mitchell\"    -    Cédula profesional. 901014585", fontSub);
            clinica.Font.SetStyle(Font.UNDERLINE);
            medico.Alignment = Element.ALIGN_CENTER;
            clinica.Alignment = Element.ALIGN_CENTER;

            doc.Add(medico);
            doc.Add(clinica);

           
            doc.Add(Chunk.NEWLINE);
            iTextSharp.text.Font fontPaciente = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            Paragraph datosPaciente = new Paragraph("Paciente: Víctor Manuel Vásquez Poblete         Edad: 22        Fecha: 04/05/2021", fontPaciente);
            doc.Add(datosPaciente);
            doc.Add(Chunk.NEWLINE);
            // Creamos una tabla que contendrá el nombre, apellido y país
            // de nuestros visitante.
            PdfPTable tblPrueba = new PdfPTable(3);
            tblPrueba.WidthPercentage = 100;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clNombre = new PdfPCell(new Phrase("Medicamento", _standardFont));
            clNombre.BorderWidth = 0;
            clNombre.FixedHeight= 17;
            clNombre.BorderWidthBottom = 0.75f;

            PdfPCell clApellido = new PdfPCell(new Phrase("Dosis", _standardFont));
            clApellido.BorderWidth = 0;
            clApellido.BorderWidthBottom = 0.75f;
            clApellido.FixedHeight = 17;

            PdfPCell clPais = new PdfPCell(new Phrase("Indicaciones", _standardFont));
            clPais.BorderWidth = 0;
            clPais.BorderWidthBottom = 0.75f;
            clPais.FixedHeight = 17;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clNombre);
            tblPrueba.AddCell(clApellido);
            tblPrueba.AddCell(clPais);

            // Llenamos la tabla con información
            clNombre = new PdfPCell(new Phrase("Paracetamol", _standardFont));
            clNombre.BorderWidth = 0;
            clNombre.PaddingTop = 8f;

            clApellido = new PdfPCell(new Phrase("20 gm", _standardFont));
            clApellido.BorderWidth = 0;
            clApellido.PaddingTop = 8f;

            clPais = new PdfPCell(new Phrase("Tomar cada 8 hrs", _standardFont));
            clPais.BorderWidth = 0;
            clPais.PaddingTop = 8f;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clNombre);
            tblPrueba.AddCell(clApellido);
            tblPrueba.AddCell(clPais);

            doc.Add(tblPrueba);

            doc.Close();
            writer.Close();
        }
    }
}