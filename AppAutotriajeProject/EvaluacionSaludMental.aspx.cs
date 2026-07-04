using System;
using System.Web.UI;

namespace AutoTriageWeb
{
    public partial class EvaluacionSaludMental : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EvaluacionMaternidad.aspx");
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            bool respuesta = bool.Parse(rblSaludMental.SelectedValue);

            // Session["EvaluacionSaludMental"] = respuesta;

            Response.Redirect("~/EvaluacionOncologica.aspx");
        }
    }
}
