using ProjectDto.Dtos.RegistroAtencionDtos;
using ProjectDto.Dtos.RespuestaPretriajeDtos;
using ProjectServices.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAutotriajeProject
{
    public partial class SalaEspera : System.Web.UI.Page
    {
        private readonly RegistroAtencionService _registroService = new RegistroAtencionService();
        private readonly RespuestaPreguntaPretriajeService _respuestaService = new RespuestaPreguntaPretriajeService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarSalaEspera();

            }
        }

        protected void tmActualizarSala_Tick(object sender, EventArgs e)
        {
            if(!pnlModalDetalles.Visible)
            {
                CargarSalaEspera();
            }
        }

        private void CargarSalaEspera()
        {
            List<SalaEsperaDto> pacientes = _registroService.ObtenerPacientesSalaEspera();

            ViewState["PacientesSalaEspera"] = pacientes;

            rptPacientesEspera.DataSource = pacientes;
            rptPacientesEspera.DataBind();
        }

        private void CargarPreguntas(int idAtencion)
        {
            List<RespuestaModalSalaEsperaDto> respuestas = _respuestaService.ObtenerRespuestasRegistro(idAtencion);

            rptPreguntas.DataSource = respuestas;
            rptPreguntas.DataBind();

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }


        // Manejo interactivo de clic en tarjetas

        protected void rptPacientesEspera_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalles")
            {
                tmActualizarSala.Enabled = false;

                int idAtencion = Convert.ToInt32(e.CommandArgument);


                List<SalaEsperaDto> pacientes = ViewState["PacientesSalaEspera"] as List<SalaEsperaDto>;

                if (pacientes == null)
                    return;

                SalaEsperaDto paciente = pacientes?.Find(x => x.IdAtencion == idAtencion);

                if (paciente != null)
                {
                    lblMotivoConsulta.Text = paciente.MotivoConsulta;

                    lblSintomaPredominante.Text = paciente.FlujoClinico;

                    lblTiempoEspera.Text = CalcularTiempoEspera(paciente.FechaRegistro);
                }


                CargarPreguntas(idAtencion);

                pnlModalDetalles.Visible = true;
            }
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            pnlModalDetalles.Visible = false;

            tmActualizarSala.Enabled = true;
            CargarSalaEspera();
        }

        private string CalcularTiempoEspera(DateTime fechaRegistro)
        {
            TimeSpan tiempo = DateTime.Now - fechaRegistro;

            if (tiempo.TotalMinutes < 1)
                return "Menos de 1 minuto";

            if (tiempo.TotalHours < 1)
            {
                int minutos = (int)tiempo.TotalMinutes;
                return minutos == 1
                    ? "1 minuto"
                    : $"{minutos} minutos";
            }

            return $"{(int)tiempo.TotalHours} h {tiempo.Minutes} min";
        }

        // Retorna la clase de prioridad (1 al 5) para aplicar el color al borde y fondo de ícono
        public string GetCssClassTarjeta(int? nivelPrioridad, bool autotriajeIniciado)
        {
            int prioridad;

            if (!autotriajeIniciado || nivelPrioridad == null)
            {
                prioridad = 0;
            }
            else
            {
                prioridad = nivelPrioridad.Value;
            }

            string claseClick = autotriajeIniciado
                ? "clickable"
                : "no-clickable";

            return $"paciente-card prioridad-{prioridad} {claseClick}";
        }

        public string GetClaseIconoFontAwesome(SalaEsperaDto dto)
        {
            if (!dto.AutotriajeIniciado)
                return "fa-regular fa-clock";

            if (dto.CondicionMaternidad)
                return "fa-solid fa-baby-carriage";

            if (dto.CondicionMental)
                return "fa-solid fa-brain";

            if (dto.CondicionOncologica)
                return "fa-solid fa-dna";

            return "fa-solid fa-circle-check";
        }

        public bool MostrarPoblacionPriorizada(SalaEsperaDto dto)
        {
            if (dto == null)
                return false;

            return dto.CondicionMaternidad
                || dto.CondicionMental
                || dto.CondicionOncologica
                || dto.EdadPaciente < 5
                || dto.EdadPaciente >= 65;
        }

    }
}