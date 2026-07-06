using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAutotriajeProject
{
    public partial class AdelantarTriage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ProcesarRespuesta(object sender, EventArgs e)
        {
            LinkButton botonPresionado = (LinkButton)sender;

            string respuestaString = botonPresionado.CommandArgument;
            bool deseaContinuar = Convert.ToBoolean(respuestaString);

            if (deseaContinuar)
            {
                // SÍ
                //...
                // Redireccionamos a motivo de consulta
                // Por el vamos a finalizacion
                Response.Redirect("~/Finalizacion.aspx");
            }
            else
            {
                // NO
                //...
                Response.Redirect("~/Finalizacion.aspx");
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EvaluacionOncologica.aspx");
        }
    }
}