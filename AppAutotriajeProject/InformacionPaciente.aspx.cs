using System;
using System.Web.UI;

namespace AutoTriageWeb
{
    public partial class InformacionPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPaciente();
                CargarGeneros();
            }
        }

        private void CargarPaciente()
        {
            // Leer Session["BusquedaPaciente"]
            bool existePaciente = false;

            if (existePaciente)
            {
                HabilitarModoConsulta();
            }
            else
            {
                HabilitarModoRegistro();
            }
            // Llenar controles
            // Mostrar u ocultar botón de editar
            // Bloquear o desbloquear controles
        }

        private void CargarGeneros()
        {
            // ddlSexoBiologico.DataSource...
            // ddlSexoBiologico.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Identificacion.aspx");
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            // Construir DTO
            // Guardar
            // Redireccionar según el caso (dejaré EvaluacionMaternidad.aspx por ahora)
            Response.Redirect("~/EvaluacionMaternidad.aspx");
        }

        private void HabilitarModoConsulta()
        {
            txtPrimerNombre.Enabled = false;
            txtSegundoNombre.Enabled = false;
            txtPrimerApellido.Enabled = false;
            txtSegundoApellido.Enabled = false;
            txtFechaNacimiento.Enabled = false;
            ddlSexoBiologico.Enabled = false;

            btnEditarPaciente.Visible = true;
        }

        private void HabilitarModoRegistro()
        {
            txtPrimerNombre.Enabled = true;
            txtSegundoNombre.Enabled = true;
            txtPrimerApellido.Enabled = true;
            txtSegundoApellido.Enabled = true;
            txtFechaNacimiento.Enabled = true;
            ddlSexoBiologico.Enabled = true;

            btnEditarPaciente.Visible = false;
        }
    }
}
