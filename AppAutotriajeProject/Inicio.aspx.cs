using ProjectData.Repositories.Interfaces;
using ProjectServices.Implementations;
using ProjectServices.Interfaces;
using System;
using System.Web.UI;

namespace AutoTriageWeb
{
    public partial class Inicio : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
      
        }

        protected void btnIniciarTriage_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Identificacion.aspx");
        }
    }
}
