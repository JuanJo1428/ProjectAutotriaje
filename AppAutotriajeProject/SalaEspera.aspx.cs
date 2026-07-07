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

        private void CargarSalaEspera()
        {
            // List<PacienteEsperaDto> listaPacientes = _salaEsperaService.ObtenerPacientes(); Aquí será lo que sea que vaya ahí, no sé de eso.

            var listaSimulada = new List<object>
            {
                new { TipoDocumento = "CC", NroDocumento = "1234567890", NombreCompleto = "Jesus Camilo Miranda Aguirre" },
                new { TipoDocumento = "TI", NroDocumento = "0987654321", NombreCompleto = "Fulano Fulanito de la Cruz" },
            };

            rptPacientesEspera.DataSource = listaSimulada;

            rptPacientesEspera.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}
