using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAutotriajeProject
{
    // Estructura de datos temporal / DTO
    public class PreguntaDTO
    {
        public int Id { get; set; }
        public string TextoPregunta { get; set; }
        public string TipoPregunta { get; set; } // "SINO" o "DROPDOWN"
        public List<string> Opciones { get; set; } // Aplica para DROPDOWN
    }

    public partial class PreguntasSeguimiento : Page
    {
        // Guardamos el índice actual en ViewState para la prueba interactiva
        private int IndicePreguntaActual
        {
            get { return ViewState["IndicePreguntaActual"] != null ? (int)ViewState["IndicePreguntaActual"] : 0; }
            set { ViewState["IndicePreguntaActual"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IndicePreguntaActual = 0;
                CargarSiguientePreguntaSimulada();
            }
        }

        /// <summary>
        /// Método principal que se encarga de renderizar la pregunta recibida.
        /// Este es el método que usará la respuesta entregada por el backend.
        /// </summary>
        public void CargarPreguntaEnPantalla(PreguntaDTO pregunta)
        {
            if (pregunta == null)
            {
                // Final del cuestionario
                divCardPregunta.Visible = false;
                pnlFinPreguntas.Visible = true;
                return;
            }

            lblTextoPregunta.Text = pregunta.TextoPregunta;

            // Ocultamos ambos esqueletos por defecto
            pnlTipoSiNo.Visible = false;
            pnlTipoDropdown.Visible = false;

            // Mostramos el esqueleto correspondiente según el tipo
            if (pregunta.TipoPregunta == "SINO")
            {
                pnlTipoSiNo.Visible = true;
            }
            else if (pregunta.TipoPregunta == "DROPDOWN")
            {
                pnlTipoDropdown.Visible = true;

                // Limpiar y cargar la lista desplegable
                ddlOpciones.Items.Clear();
                ddlOpciones.Items.Add(new ListItem("-- Seleccione una opción --", ""));

                if (pregunta.Opciones != null)
                {
                    foreach (string opcion in pregunta.Opciones)
                    {
                        ddlOpciones.Items.Add(new ListItem(opcion, opcion));
                    }
                }
            }
        }

        // Evento cuando se responde una pregunta tipo Sí / No
        protected void btnOpcionSiNo_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string respuesta = btn.CommandArgument; // Contendrá "SI" o "NO"

            AvanzarSimulacion();
        }

        // Evento cuando se responde una pregunta tipo Lista Desplegable
        protected void btnSiguienteDropdown_Click(object sender, EventArgs e)
        {
            string respuesta = ddlOpciones.SelectedValue;

            if (string.IsNullOrEmpty(respuesta))
            {
                // Validación básica opcional
                return;
            }

            AvanzarSimulacion();
        }

        #region MÉTODOS DE PRUEBA SIMULADA (Comentar/Eliminar al integrar el backend real)

        private void AvanzarSimulacion()
        {
            IndicePreguntaActual++;
            CargarSiguientePreguntaSimulada();
        }

        private void CargarSiguientePreguntaSimulada()
        {
            List<PreguntaDTO> bancoPruebas = ObtenerPreguntasDePrueba();

            if (IndicePreguntaActual < bancoPruebas.Count)
            {
                CargarPreguntaEnPantalla(bancoPruebas[IndicePreguntaActual]);
            }
            else
            {
                CargarPreguntaEnPantalla(null); // Indica el final
            }
        }

        private List<PreguntaDTO> ObtenerPreguntasDePrueba()
        {
            return new List<PreguntaDTO>
            {
                new PreguntaDTO
                {
                    Id = 1,
                    TextoPregunta = "¿Ha presentado fiebre o escalofríos en las últimas 24 horas?",
                    TipoPregunta = "SINO"
                },
                new PreguntaDTO
                {
                    Id = 2,
                    TextoPregunta = "¿Cuál es el nivel aproximado de intensidad de su dolor?",
                    TipoPregunta = "DROPDOWN",
                    Opciones = new List<string> { "Leve (1 - 3)", "Moderado (4 - 6)", "Severo (7 - 9)", "Insoportable (10)" }
                },
                new PreguntaDTO
                {
                    Id = 3,
                    TextoPregunta = "¿Siente alguna dificultad para respirar en este momento?",
                    TipoPregunta = "SINO"
                },
                new PreguntaDTO
                {
                    Id = 4,
                    TextoPregunta = "¿Hace cuánto tiempo iniciaron los síntomas principales?",
                    TipoPregunta = "DROPDOWN",
                    Opciones = new List<string> { "Menos de 2 horas", "Entre 2 y 12 horas", "Entre 12 y 24 horas", "Más de 24 horas" }
                }
            };
        }

        #endregion
    }
}