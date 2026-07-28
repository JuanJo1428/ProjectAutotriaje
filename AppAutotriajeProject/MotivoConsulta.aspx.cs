using ProjectDto.Dtos.PretriajeDtos;
using ProjectDto.Dtos.RegistroAtencionDtos;
using ProjectServices.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace AppAutotriajeProject
{
    public partial class MotivoConsulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Si el usuario ya había escrito un motivo de consulta previo, lo recuperamos en la caja de texto
                SolicitudPretriajeDto motivoExistente = Session["MotivoConsulta"] as SolicitudPretriajeDto;

                if (motivoExistente != null && !string.IsNullOrWhiteSpace(motivoExistente.MotivoConsulta))
                {
                    txtSintomas.Text = motivoExistente.MotivoConsulta;
                }
            }
        }

        private readonly PretriajeService _pretriajeService = new PretriajeService();
        protected async void btnContinuar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            if (string.IsNullOrWhiteSpace(txtSintomas.Text))
                return;

            //Creo la solicitud en Dto
            SolicitudPretriajeDto motivoConsulta = new SolicitudPretriajeDto
            {
                MotivoConsulta = txtSintomas.Text
            };

            
            Session["MotivoConsulta"] = motivoConsulta;


            //Obtiene el flujo mediante la IA
            FlujoPretriajeDto flujoSeleccionado = await _pretriajeService.DeterminarFlujoAsync(motivoConsulta);

            if (flujoSeleccionado == null)
            {
                //Mensaje de error

                return;
            }

            Session["FlujoClinico"] = flujoSeleccionado;


            //Obtengo Primera Pregunta
            PreguntaPretriajeDto primeraPregunta = _pretriajeService.ObtenerPrimeraPregunta(flujoSeleccionado.IdFlujo);

            if (primeraPregunta == null)
            {
                //Error

                return;
            }

            Session["PreguntaActual"] = primeraPregunta;


            Response.Redirect("~/PreguntasSeguimiento.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            ConsultarRegistroPendienteRespuestaDto registroAtencion = Session["RegistroPendiente"] as ConsultarRegistroPendienteRespuestaDto;
            
            if (registroAtencion.TieneRegistroPendiente == true)
            {
                Response.Redirect("~/RegistroPendiente.aspx");
            }

            Response.Redirect("~/AdelantarTriage.aspx");
        }

    }
}
