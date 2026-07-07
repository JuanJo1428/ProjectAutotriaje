using System;
using System.Web.UI;

namespace AppAutotriajeProject
{
    public partial class Inicio : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Identificacion.aspx");
        }

        protected void btnWaitingRoom_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SalaEspera.aspx");
        }
    }
}
