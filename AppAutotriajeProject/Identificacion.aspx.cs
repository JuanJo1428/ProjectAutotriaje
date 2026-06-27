using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace AutoTriageWeb
{
    public partial class Identificacion : System.Web.UI.Page
    {
        // Propiedad accesible desde la página .aspx para pasar las reglas a JS
        protected string ReglasDocumentoJson { get; set; } = "{}";

        protected void Page_Load(object sender, EventArgs e)
        {
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

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string tipoSeleccionado = ddlTipoDocumento.SelectedValue;
                string documentoIngresado = txtDocumento.Text;

                //...

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
    }

    internal class ReglaConfig
    {
        public string Nombre { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
    }
}
