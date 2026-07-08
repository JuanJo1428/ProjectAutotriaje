using ProjectServices.Implementations;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace AppAutotriajeProject
{
    public partial class SalaEspera : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarSalaEspera();
            }
        }


        private readonly RegistroAtencionService _registroService = new RegistroAtencionService();
        private void CargarSalaEspera()
        {
            var pacientes = _registroService.ObtenerPacientesSalaEspera();

            rptPacientesEspera.DataSource = pacientes;

            rptPacientesEspera.DataBind();
        }


        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

    }
}
