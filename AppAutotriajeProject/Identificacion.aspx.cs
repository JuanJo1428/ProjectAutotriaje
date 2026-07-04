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
            // Validar usando las reglas obtenidas desde la base de datos.
            args.IsValid = false;
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
            ddlTipoDocumento.DataSource = _tipoDocumentoService.ObtenerTiposDocumento();
            ddlTipoDocumento.DataBind();

            // INTENTO DE ESTRUCTURA
            foreach (ListItem item in ddlTipoDocumento.Items)
            {
                // Reemplazar por valores una vez ya se obtengan los datos suando el objeto correspondiente
                item.Attributes["data-min"] = "0";
                item.Attributes["data-max"] = "0"; 
            }

            ddlTipoDocumento.Items.Insert(0,new ListItem("Seleccione tipo de documento", ""));
            ddlTipoDocumento.SelectedIndex = 0;
        }
    }
}
