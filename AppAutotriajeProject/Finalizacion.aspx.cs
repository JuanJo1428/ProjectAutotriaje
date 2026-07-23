using ProjectDto.Dtos.PretriajeDtos;
using ProjectDto.Dtos.RegistroAtencionDtos;
using ProjectServices.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAutotriajeProject
{
    public partial class Finalizacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private readonly RegistroAtencionService _registroAtencionService = new RegistroAtencionService();
        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            RegistroAtencionDto registroAtencion = Session["RegistroAtencion"] as RegistroAtencionDto;

            if (registroAtencion == null)
            {
                //Error
                return;
            }

            //Sinó realizó el Autotriaje Clínico no se actualiza el registro de atención
            if (!registroAtencion.AutotriajeIniciado)
            {
                Session.Clear();
                Response.Redirect("~/Default.aspx");
                return;
            }

            //Validar Sessions
            SolicitudPretriajeDto motivoDto = Session["MotivoConsulta"] as SolicitudPretriajeDto;

            FlujoPretriajeDto flujoDto = Session["FlujoClinico"] as FlujoPretriajeDto;

            ResultadoPretriajeDto resultadoDto = Session["ResultadoPretriaje"] as ResultadoPretriajeDto;

            if (motivoDto == null || flujoDto == null || resultadoDto == null)
            {
                // Manejar error
                return;
            }

         
            string motivoConsulta = motivoDto.MotivoConsulta;

            int flujoPretriaje = flujoDto.IdFlujo;

            int prioridad = resultadoDto.Prioridad.IdPrioridad;


            ActualizarRegistroAtencionDto registroActualizado = new ActualizarRegistroAtencionDto 
                { 
                    IdAtencion = registroAtencion.IdAtencion,

                    MotivoConsulta = motivoConsulta,

                    AutotriajeIniciado = true,

                    IdFlujoClinico = flujoPretriaje,

                    IdPrioridad = prioridad,

                    Atendido = false
              
                };

            _registroAtencionService.ActualizarRegistro(registroActualizado);

            Session.Clear();
            Response.Redirect("~/Default.aspx");
        }
    }
}