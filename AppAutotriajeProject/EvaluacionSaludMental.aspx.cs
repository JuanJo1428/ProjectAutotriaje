using ProjectDto.Dtos;
using ProjectDto.Dtos.RegistroAtencionDtos;
using ProjectCommon.Constants;
using System;
using System.Web.UI;

namespace AppAutotriajeProject
{
    public partial class EvaluacionSaludMental : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            PacienteProcesadoRespuestaDto respuestaProcesado = Session["PacienteProcesado"] as PacienteProcesadoRespuestaDto;

            if (respuestaProcesado.Paciente.IdGenero == (int)Generos.Masculino)
            {
                Response.Redirect("~/InformacionPaciente.aspx");
                return;
            }

            Response.Redirect("~/EvaluacionMaternidad.aspx");
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            EvaluacionesPacienteDto evaluaciones = Session["EvaluacionesPaciente"] as EvaluacionesPacienteDto;

            if (evaluaciones == null)
            {
                Response.Redirect("~/Identificacion.aspx");
                return;
            }

            evaluaciones.CondicionMental = bool.Parse(rblSaludMental.SelectedValue);

            Session["EvaluacionesPaciente"] = evaluaciones;

            Response.Redirect("~/EvaluacionOncologica.aspx");

            return;
        }
    }
}
