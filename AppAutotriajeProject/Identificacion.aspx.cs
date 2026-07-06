using ProjectData.Entities;
using ProjectDto.Dtos;
using ProjectServices.Implementations;
using ProjectServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAutotriajeProject
{
    public partial class Identificacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //CargarTiposDocumento();

                if (Session["MensajeError"] != null)
                {
                    lblMensajeError.Text = Session["MensajeError"].ToString();
                    lblMensajeError.Visible = true;

                    Session.Remove("MensajeError");
                }
            }
        }

        protected void cvDocumento_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(ddlTipoDocumento.SelectedValue))
            {
                args.IsValid = false;
                return;
            }

            int idTipoDocumento =
                Convert.ToInt32(ddlTipoDocumento.SelectedValue);

            TipoDocumentoListaDto tipoDocumento =
                _tipoDocumentoService.ObtenerTipoDocumento(idTipoDocumento);

            if (tipoDocumento == null)
            {
                args.IsValid = false;
                return;
            }

            int longitud = args.Value.Trim().Length;

            args.IsValid =
                longitud >= tipoDocumento.MinLength &&
                longitud <= tipoDocumento.MaxLength;
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

                Response.Redirect("~/InformacionPaciente.aspx");
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

            ddlTipoDocumento.Items.Insert(0, new ListItem("Seleccione tipo de documento", "") );

            ddlTipoDocumento.SelectedIndex = 0;
        }
    }
}
