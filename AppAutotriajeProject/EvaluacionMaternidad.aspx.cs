using ProjectDto.Dtos.RegistroAtencionDtos;
using System;
using System.Web.UI;

namespace AppAutotriajeProject
{
    public partial class EvaluacionMaternidad : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CondicionMaternidad"] != null)
            {
                bool maternidad = (bool)Session["CondicionMaternidad"];

                rblMaternidad.SelectedValue = maternidad.ToString().ToLower();
            }
                
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/InformacionPaciente.aspx");
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            EvaluacionesPacienteDto evaluaciones = new EvaluacionesPacienteDto();

            evaluaciones.CondicionMaternidad = bool.Parse(rblMaternidad.SelectedValue);

            Session["CondicionMaternidad"] = evaluaciones.CondicionMaternidad;

            Session["EvaluacionesPaciente"] = evaluaciones;

            Response.Redirect("~/EvaluacionSaludMental.aspx");

            return;
        }
    }
}