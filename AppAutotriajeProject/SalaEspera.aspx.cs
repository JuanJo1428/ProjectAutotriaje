using ProjectServices.Implementations;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace AppAutotriajeProject
{
    public partial class SalaEspera : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarSalaEspera();
            
            }
        }


        private readonly RegistroAtencionService _registroService = new RegistroAtencionService();
        private void CargarSalaEspera()
        {
            //Elementos necesarios para la creacion de la tarjeta del repeater para el paciente
            //Cada registro tambien tenes el nivelPrioridad(1-5), con su respectivo color y codigoColor
            //Tiene tambien el MotivoConsulta en string
            //Condiciones de priorizacion para colocar los respectivos simbolos representativos

            var pacientes = _registroService.ObtenerPacientesSalaEspera();

            rptPacientesEspera.DataSource = pacientes;

            rptPacientesEspera.DataBind();

            //btnVerDetalles.CommandArgument = pacientes.IdRegistro;
        }

        private readonly RespuestaPreguntaPretriajeService _respuestaService = new RespuestaPreguntaPretriajeService();
        private void CargarPreguntas(int idRegistro)
        {
            var respuestas = _respuestaService.ObtenerRespuestasRegistro(idRegistro);

            //rptPreguntas.DataSource = respuestas;

            //rptPreguntas.DataBind();

        }

        protected void btnVerDetalles_Click(object sender, EventArgs e)
        {
            //El command del boton de ver detalles en la tarjeta del repeater es el idRegistro que obtiene cuando carga el paciente
            //Lo puede poner dinamicamente
            //int idRegistro = int.Parse((btn.CommandArgument));

            //CargarPreguntas(idRegistro);

            //Cuando este creada, intentar que sea una pestaña emergente(no se como sea su funcionamiento)
            Response.Redirect("~/VerDetalles.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

    }
}
