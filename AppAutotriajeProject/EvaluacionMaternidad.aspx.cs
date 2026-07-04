using System;
using System.Web.UI;

namespace AutoTriageWeb
{
    public partial class EvaluacionMaternidad : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/InformacionPaciente.aspx");
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            bool respuesta = bool.Parse(rblMaternidad.SelectedValue);

            // Session["EvaluacionMaternidad"] = respuesta;

            Response.Redirect("~/EvaluacionSaludMental.aspx");
        }
    }
}