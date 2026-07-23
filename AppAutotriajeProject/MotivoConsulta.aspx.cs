using ProjectDto.Dtos.PretriajeDtos;
using ProjectServices.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAutotriajeProject
{
    public partial class MotivoConsulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
            Response.Redirect("~/AdelantarTriage.aspx");
        }
    }
}
