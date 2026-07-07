using ProjectDto.Dtos.RegistroAtencionDtos;
using System;
using System.Web.UI;

namespace AppAutotriajeProject
{
    public partial class EvaluacionOncologica : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EvaluacionSaludMental.aspx");
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

            evaluaciones.CondicionOncologica = bool.Parse(rblOncologica.SelectedValue);

            Session["EvaluacionesPaciente"] = evaluaciones;

            Response.Redirect("~/AdelantarTriage.aspx");

            return;
        }
    }
}
