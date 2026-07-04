using ProjectData.Entities;
using ProjectDto.Dtos;
using ProjectServices.Implementations;
using ProjectServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutoTriageWeb
{
    public partial class Identificacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTiposDocumento();

            }

        }

        protected void cvDocumento_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(ddlTipoDocumento.SelectedValue))
            {
                args.IsValid = false;
                return;
            }

            ListItem item = ddlTipoDocumento.SelectedItem;

            if (!int.TryParse(item.Attributes["data-min"], out int min) ||
                !int.TryParse(item.Attributes["data-max"], out int max))
            {
                args.IsValid = false;
                return;
            }

            int longitud = args.Value.Trim().Length;

            args.IsValid = longitud >= min && longitud <= max;
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
            Response.Redirect("~/Default.aspx");
        }

        
        protected void lnkEscaneo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EscaneoDocumento.aspx");
        }


        private readonly TipoDocumentoService _tipoDocumentoService = new TipoDocumentoService();
        private void CargarTiposDocumento()
        {
            List<TipoDocumentoListaDto> tiposDocumento = _tipoDocumentoService.ObtenerTiposDocumento();

            ddlTipoDocumento.DataSource = tiposDocumento;
            ddlTipoDocumento.DataBind();

            ddlTipoDocumento.Items.Insert(0,
                new ListItem("Seleccione tipo de documento", ""));

            for (int i = 0; i < tiposDocumento.Count; i++)
            {
                ddlTipoDocumento.Items[i + 1].Attributes["data-min"] =
                    tiposDocumento[i].MinLength.ToString();

                ddlTipoDocumento.Items[i + 1].Attributes["data-max"] =
                    tiposDocumento[i].MaxLength.ToString();
            }

            ddlTipoDocumento.SelectedIndex = 0;
        }
    }
}
