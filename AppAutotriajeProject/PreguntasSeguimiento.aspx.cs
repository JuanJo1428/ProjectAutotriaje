using Microsoft.Win32;
using ProjectCommon.Constants;
using ProjectData.Entities;
using ProjectDto.Dtos.PretriajeDtos;
using ProjectDto.Dtos.RegistroAtencionDtos;
using ProjectServices.Implementations;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAutotriajeProject
{

    public partial class PreguntasSeguimiento : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PreguntaPretriajeDto pregunta = Session["PreguntaActual"] as PreguntaPretriajeDto;

                if (pregunta == null)
                {
                    Response.Redirect("~/MotivoConsulta.aspx");
                    return;
                }

                CargarPreguntaEnPantalla(pregunta);
            }
        }


        private readonly PretriajeService _pretriajeService = new PretriajeService();
        public void CargarPreguntaEnPantalla(PreguntaPretriajeDto pregunta)
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
            pnlTipoLista.Visible = false;


            // Mostramos el esqueleto correspondiente según el tipo
            if (pregunta.TipoRespuesta == TipoRespuesta.SiNo)
            {
                pnlTipoSiNo.Visible = true;

                if (pregunta.Opciones != null && pregunta.Opciones.Count == 2)
                {
                    btnSi.Text = pregunta.Opciones[0].Texto;
                    btnSi.CommandArgument = pregunta.Opciones[0].IdOpcion.ToString();

                    btnNo.Text = pregunta.Opciones[1].Texto;
                    btnNo.CommandArgument = pregunta.Opciones[1].IdOpcion.ToString();
                }
            }

            else if (pregunta.TipoRespuesta == TipoRespuesta.Lista)
            {
                pnlTipoLista.Visible = true;

                rptOpciones.DataSource = pregunta.Opciones;
                rptOpciones.DataBind();
            }
        }

        private readonly RespuestaPreguntaPretriajeService _respuestaService = new RespuestaPreguntaPretriajeService();
        // Evento cuando se responde una pregunta tipo Sí / No
        protected void btnOpcionSiNo_Click(object sender, EventArgs e)
        {

            PreguntaPretriajeDto pregunta = Session["PreguntaActual"] as PreguntaPretriajeDto;

            if (pregunta == null)
                return;

            Button btn = (Button)sender;


            RegistrarRespuestaPreguntaDto respuesta = ConstruirResgistrarRespuestaDto
                (pregunta.IdPregunta, int.Parse(btn.CommandArgument));

            //Guardamos la pregunta y la respuesta
            _respuestaService.RegistrarRespuesta(respuesta);


            ProcesarRespuesta(respuesta);
        }

        protected void rptOpciones_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "Seleccionar")
                return;

            PreguntaPretriajeDto pregunta = Session["PreguntaActual"] as PreguntaPretriajeDto;

            if (pregunta == null)
                return;

            RegistrarRespuestaPreguntaDto respuesta =
                ConstruirResgistrarRespuestaDto(pregunta.IdPregunta, int.Parse(e.CommandArgument.ToString()));

            _respuestaService.RegistrarRespuesta(respuesta);

            ProcesarRespuesta(respuesta);
        }

        //Para procesar cualquier tipo de respuesta
        private void ProcesarRespuesta(RegistrarRespuestaPreguntaDto respuesta)
        {
            ResultadoPretriajeDto resultado = _pretriajeService.ProcesarRespuesta(respuesta);

            if (resultado == null)
                return;

            if (resultado.Finalizado)
            {
                Session["ResultadoPretriaje"] = resultado;

                Response.Redirect("~/Finalizacion.aspx");
                return;
            }

            Session["PreguntaActual"] = resultado.SiguientePregunta;

            CargarPreguntaEnPantalla(resultado.SiguientePregunta);
        }

        private RegistrarRespuestaPreguntaDto ConstruirResgistrarRespuestaDto(int idPregunta, int idOpcionSeleccionada)
        {
            RegistroAtencionDto registroAtencion = Session["RegistroAtencion"] as RegistroAtencionDto;

            return new RegistrarRespuestaPreguntaDto
            {
                IdRegistro = registroAtencion.IdAtencion,

                IdPregunta = idPregunta,

                IdOpcionSeleccionada = idOpcionSeleccionada,

            };
        }

    }
}
