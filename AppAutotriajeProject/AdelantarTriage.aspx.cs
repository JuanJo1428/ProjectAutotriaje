using ProjectDto.Dtos;
using ProjectDto.Dtos.RegistroAtencionDtos;
using ProjectServices.Implementations;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAutotriajeProject
{
    public partial class AdelantarTriage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private readonly RegistroAtencionService _registroService = new RegistroAtencionService();
        protected void ProcesarRespuesta(object sender, EventArgs e)
        {

            LinkButton botonPresionado = (LinkButton)sender;

            string respuestaString = botonPresionado.CommandArgument;
            bool deseaContinuar = Convert.ToBoolean(respuestaString);


            //Crear el Dto para la creación del registro
            CrearRegistroAtencionDto datosRegistro = ConstruirCrearRegistroDto(deseaContinuar);

            if (datosRegistro == null)
            {
                Response.Redirect("~/Identificacion.aspx");
                return;
            }


            //Se crea y se guarda el registro en la sesión del usuario
            CrearRegistroAtencionRespuestaDto respuesta = _registroService.CrearRegistroAtencion(datosRegistro);

            Session["RegistroAtencion"] = respuesta.RegistroAtencion;

            Session["CrearRegistroRespuesta"] = respuesta;


            if (deseaContinuar)
            {
                //Mientras se crea MotivoConsulta.aspx
                Response.Redirect("~/Finalizacion.aspx");
                return;
            }

            Response.Redirect("~/Finalizacion.aspx");
            return;
            
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EvaluacionOncologica.aspx");
        }

        private CrearRegistroAtencionDto ConstruirCrearRegistroDto(bool deseaAdelantarAutotriaje)
        {
            //Recibe la información del paciente procesada
            PacienteProcesadoRespuestaDto pacienteProcesado = Session["PacienteProcesado"] as PacienteProcesadoRespuestaDto;

            if (pacienteProcesado == null ||
                !pacienteProcesado.Paciente.IdPaciente.HasValue)
            {
                return null;
            }


            //Recibe las condiciones de priorización validadas
            EvaluacionesPacienteDto evaluaciones = Session["EvaluacionesPaciente"] as EvaluacionesPacienteDto;

            if (evaluaciones == null)
            {
                return null;
            }

            return new CrearRegistroAtencionDto
            {
                IdPaciente = pacienteProcesado.Paciente.IdPaciente.Value,

                CondicionMaternidad = evaluaciones.CondicionMaternidad,

                CondicionMental = evaluaciones.CondicionMental,

                CondicionOncologica = evaluaciones.CondicionOncologica,

                AutotriajeIniciado = deseaAdelantarAutotriaje
            };
        }

    }
}