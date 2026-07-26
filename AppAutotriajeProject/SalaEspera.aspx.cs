using ProjectServices.Implementations;
using System;
using System.Collections.Generic;
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
                // Para entorno de producción:
                // CargarSalaEspera();

                // Para pruebas locales:
                CargarDatosSimulados();
            }
        }

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

        private void CargarPreguntas(int idRegistro)
        {
            // Para entorno de producción:
            // var respuestas = _respuestaService.ObtenerRespuestasRegistro(idRegistro);
            // rptPreguntas.DataSource = respuestas;
            // rptPreguntas.DataBind();

            var respuestasSimuladas = ObtenerRespuestasSimuladas(idRegistro);
            rptPreguntas.DataSource = respuestasSimuladas;
            rptPreguntas.DataBind();
        }

        /* 
        // Se reemplazó por rptPacientesEspera_ItemCommand para abrir el modal sobre la misma página.
        protected void btnVerDetalles_Click(object sender, EventArgs e)
        {
            //El command del boton de ver detalles en la tarjeta del repeater es el idRegistro que obtiene cuando carga el paciente
            //Lo puede poner dinamicamente
            //int idRegistro = int.Parse((btn.CommandArgument));

            //CargarPreguntas(idRegistro);

            //Cuando este creada, intentar que sea una pestaña emergente(no se como sea su funcionamiento)
            Response.Redirect("~/VerDetalles.aspx");
        }
        */

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        // Manejo interactivo de clic en tarjetas
        protected void rptPacientesEspera_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalles")
            {
                int idRegistro = Convert.ToInt32(e.CommandArgument);
                CargarPreguntas(idRegistro);
                pnlModalDetalles.Visible = true;
            }
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            pnlModalDetalles.Visible = false;
        }

        #region HELPERS PARA CLASES DINÁMICAS DE CSS E ÍCONOS

        // Retorna la clase de prioridad (1 al 5) para aplicar el color al borde y fondo de ícono
        public string GetCssClassTarjeta(object nivelPrioridadObj, bool continuoTriage)
        {
            int nivelPrioridad = 4; // Prioridad por defecto (verde)

            if (nivelPrioridadObj != null && int.TryParse(nivelPrioridadObj.ToString(), out int prio))
            {
                if (prio >= 1 && prio <= 5) nivelPrioridad = prio;
            }

            string claseClick = continuoTriage ? "clickable" : "no-clickable";
            return $"paciente-card prioridad-{nivelPrioridad} {claseClick}";
        }

        public string GetClaseIconoFontAwesome(string condicion)
        {
            if (string.IsNullOrEmpty(condicion)) return "fa-solid fa-circle-check";

            switch (condicion.ToLower())
            {
                case "maternidad":
                    return "fa-solid fa-person-pregnant";
                case "saludmental":
                    return "fa-solid fa-brain";
                case "oncologica":
                    return "fa-solid fa-dna";
                default:
                    return "fa-solid fa-circle-check";
            }
        }

        #endregion

        #region MOCK DE PRUEBAS LOCALES

        private void CargarDatosSimulados()
        {
            var pacientesSimulados = new List<object>
            {
                new {
                    IdRegistro = 101,
                    Paciente = new { TipoDocumento = new { Nombre = "CC" }, NroDocumento = "1015189591", NombreCompleto = "FULANA 1" },
                    MotivoConsulta = "Dolor abdominal agudo",
                    Condicion = "Maternidad",
                    NivelPrioridad = 1,
                    ContinuoTriage = true
                },
                new {
                    IdRegistro = 102,
                    Paciente = new { TipoDocumento = new { Nombre = "CC" }, NroDocumento = "1032456789", NombreCompleto = "FULANO 2" },
                    MotivoConsulta = "Ansiedad e insomnio prolongado",
                    Condicion = "SaludMental",
                    NivelPrioridad = 2,
                    ContinuoTriage = true
                },
                new {
                    IdRegistro = 103,
                    Paciente = new { TipoDocumento = new { Nombre = "CC" }, NroDocumento = "1015189591", NombreCompleto = "FULANO 3" },
                    MotivoConsulta = "Dolor abdominal agudo",
                    Condicion = "Maternidad",
                    NivelPrioridad = 3,
                    ContinuoTriage = true
                },
                new {
                    IdRegistro = 104,
                    Paciente = new { TipoDocumento = new { Nombre = "CC" }, NroDocumento = "9876543210", NombreCompleto = "FULANO 4" },
                    MotivoConsulta = "Control de quimioterapia",
                    Condicion = "Oncologica",
                    NivelPrioridad = 5,
                    ContinuoTriage = true
                },
                new {
                    IdRegistro = 105,
                    Paciente = new { TipoDocumento = new { Nombre = "CC" }, NroDocumento = "1122334455", NombreCompleto = "FULANO 5" },
                    MotivoConsulta = "Consulta general por malestar",
                    Condicion = "Ninguna",
                    NivelPrioridad = 4,
                    ContinuoTriage = false // Hace que no sea clickeable ni abre modal
                },
                new {
                    IdRegistro = 106,
                    Paciente = new { TipoDocumento = new { Nombre = "CC" }, NroDocumento = "9876543210", NombreCompleto = "FULANO 6" },
                    MotivoConsulta = "Control de quimioterapia",
                    Condicion = "Oncologica",
                    NivelPrioridad = 4,
                    ContinuoTriage = true
                }
            };

            rptPacientesEspera.DataSource = pacientesSimulados;
            rptPacientesEspera.DataBind();
        }

        private List<object> ObtenerRespuestasSimuladas(int idRegistro)
        {
            return new List<object>
            {
                new { Pregunta = "¿Motivo de consulta registrado?", Respuesta = "Dolor fuerte en el abdomen desde la mañana." },
                new { Pregunta = "¿Fiebre en las últimas 24h?", Respuesta = "Sí" },
                new { Pregunta = "Nivel de dolor", Respuesta = "Severo (7 - 9)" }
            };
        }

        #endregion
    }
}