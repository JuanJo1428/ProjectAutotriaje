using Newtonsoft.Json;
using ProjectData.Entities;
using ProjectDto.Dtos;
using ProjectServices.Implementations;
using ProjectServices.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutoTriageWeb
{
    public partial class Identificacion : System.Web.UI.Page
    {
        // Propiedad accesible desde la página .aspx para pasar las reglas a JS
        protected string ReglasDocumentoJson { get; set; } = "{}";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTiposDocumento();

            }

            CargarReglasConfiguracion();
        }

        private void CargarReglasConfiguracion()
        {
            try
            {
                string path = Server.MapPath("~/Config/ReglasDocumento.json");
                if (File.Exists(path))
                {
                    ReglasDocumentoJson = File.ReadAllText(path);
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores silencioso o log de sistema según la política del equipo
                ReglasDocumentoJson = "{}";
            }
        }

        protected void cvDocumento_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string tipoDoc = ddlTipoDocumento.SelectedValue;
            string numDoc = args.Value;

            if (string.IsNullOrEmpty(tipoDoc) || string.IsNullOrEmpty(numDoc))
            {
                args.IsValid = false;
                return;
            }

            try
            {
                string path = Server.MapPath("~/Config/ReglasDocumento.json");
                var config = JsonConvert.DeserializeAnonymousType(File.ReadAllText(path), new
                {
                    TiposDocumento = new System.Collections.Generic.Dictionary<string, ReglaConfig>()
                });

                if (config != null && config.TiposDocumento.ContainsKey(tipoDoc))
                {
                    var regla = config.TiposDocumento[tipoDoc];
                    args.IsValid = numDoc.Length >= regla.MinLength && numDoc.Length <= regla.MaxLength;
                }
                else
                {
                    args.IsValid = false;
                }
            }
            catch
            {
                args.IsValid = false;
            }
        }


        private readonly PacienteService _pacienteService = new PacienteService();
        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string tipoSeleccionado = ddlTipoDocumento.SelectedValue;
                string documentoIngresado = txtDocumento.Text;


                BuscarPacienteDto datosBusqueda = new BuscarPacienteDto
                {
                    IdTipoDocumento = Convert.ToInt32(tipoSeleccionado),
                    NroDocumento = documentoIngresado
                };

                BuscarPacienteRespuestaDto respuesta = _pacienteService.BuscarPaciente(datosBusqueda);


                Session["BusquedaPaciente"] = respuesta;

                Response.Redirect("~/Ejemplo.aspx");
            }
        }


        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Inicio.aspx");
        }


        protected void lnkEscaneo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EscaneoDocumento.aspx");
        }


        private readonly TipoDocumentoService _tipoDocumentoService = new TipoDocumentoService();
        private void CargarTiposDocumento()
        {
            ddlTipoDocumento.DataSource = _tipoDocumentoService.ObtenerTiposDocumento();

            ddlTipoDocumento.DataBind();


            ddlTipoDocumento.Items.Insert(0,new ListItem("Seleccione tipo de documento", ""));
            ddlTipoDocumento.SelectedIndex = 0;
        }
    }

    internal class ReglaConfig
    {
        public string Nombre { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
    }
}
