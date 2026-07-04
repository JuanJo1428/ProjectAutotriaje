using System;
using System.Web.UI;

namespace AutoTriageWeb
{
    public partial class EvaluacionOncologica : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EvaluacionSaludMental.aspx");
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            bool respuesta = bool.Parse(rblOncologica.SelectedValue);

            // Session["EvaluacionOncologica"] = respuesta;

            Response.Redirect("~/Ejemplo.aspx");
        }
    }
}
