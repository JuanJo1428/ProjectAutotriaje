using ProjectDto.Dtos.RegistroAtencionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAutotriajeProject
{
    public partial class RegistroPendiente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ProcesarRespuesta(object sender, EventArgs e)
        {

            LinkButton botonPresionado = (LinkButton)sender;

            string respuestaString = botonPresionado.CommandArgument;
            bool deseaContinuar = Convert.ToBoolean(respuestaString);

            //...

            if (deseaContinuar)
            {
                Response.Redirect("~/MotivoConsulta.aspx");
                return;
            }

            Response.Redirect("~/Finalizacion.aspx");
            return;

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/InformacionPaciente.aspx");
        }
    }
}