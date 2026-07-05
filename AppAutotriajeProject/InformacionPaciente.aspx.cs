using ProjectDto.Dtos;
using ProjectDto.Dtos.RegistroAtencionDtos;
using ProjectServices.Implementations;
using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectServices.Constants;

namespace AutoTriageWeb
{
    public partial class InformacionPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGeneros();
                CargarPaciente();
                
            }
        }

        private void CargarPaciente()
        {
            BuscarPacienteRespuestaDto respuesta = Session["BusquedaPaciente"] as BuscarPacienteRespuestaDto;

            if (respuesta == null)
            {
                Response.Redirect("~/Identificacion.aspx");
                return;
            }

            PacienteDto paciente = respuesta.PacientePrincipal;

            txtTipoDocumento.Text = paciente.DescripcionTipoDocumento;
            txtNumeroDocumento.Text = paciente.NroDocumento;


            if (respuesta.Existe)
            {
                txtPrimerNombre.Text = paciente.PrimerNombre;
                txtSegundoNombre.Text = paciente.SegundoNombre;
                txtPrimerApellido.Text = paciente.PrimerApellido;
                txtSegundoApellido.Text = paciente.SegundoApellido;

                txtFechaNacimiento.Text = paciente.FechaNacimiento.ToString("yyyy-MM-dd");

                if (paciente.IdGenero > 0)
                {
                    ddlSexoBiologico.SelectedValue = paciente.IdGenero.ToString();
                }

                HabilitarModoConsulta();
            }
            else
            {
                HabilitarModoRegistro();
            }
        }


        private readonly GeneroService _generoService = new GeneroService();
        private void CargarGeneros()
        {
            ddlSexoBiologico.DataSource = _generoService.ObtenerGeneros();

            ddlSexoBiologico.DataBind();

            ddlSexoBiologico.Items.Insert(0, new ListItem("Seleccione sexo biológico", ""));

            ddlSexoBiologico.SelectedIndex = 0;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Identificacion.aspx");
        }


        private readonly PacienteService _pacienteService = new PacienteService();
        private readonly RegistroAtencionService _registroService = new RegistroAtencionService();
        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

           
            BuscarPacienteRespuestaDto respuestaBusqueda = Session["BusquedaPaciente"] as BuscarPacienteRespuestaDto;

            if (respuestaBusqueda == null)
            {
                Response.Redirect("~/Identificacion.aspx");
                return;
            }


            PacienteDto paciente = respuestaBusqueda.PacientePrincipal;

            //Creación del Dto que se procesará
            PacienteValidadoDto pacienteValidado = new PacienteValidadoDto
            {
                IdTipoDocumento = paciente.IdTipoDocumento,

                NroDocumento = paciente.NroDocumento,

                PrimerNombre = txtPrimerNombre.Text.Trim(),

                SegundoNombre = txtSegundoNombre.Text.Trim(),

                PrimerApellido = txtPrimerApellido.Text.Trim(),

                SegundoApellido = txtSegundoApellido.Text.Trim(),

                IdGenero = Convert.ToInt32(ddlSexoBiologico.SelectedValue),

                FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text)
            };

            //Procesa Paciente
            PacienteProcesadoRespuestaDto pacienteProcesado = _pacienteService.ProcesarPaciente(pacienteValidado);

            Session["PacienteProcesado"] = pacienteProcesado;


            //Consulta Registro Pendiente
            ConsultarRegistroPendienteRespuestaDto registroPendiente = _registroService.ConsultarRegistroPendiente(pacienteProcesado);

            if (registroPendiente.TieneRegistroPendiente)
            {
                Session["RegistroPendiente"] = registroPendiente;

                Response.Redirect("~/RegistroPendiente.aspx");
                return;
            }


            if (pacienteProcesado.Paciente.IdGenero == (int)Generos.Femenino)
            {
                Response.Redirect("~/EvaluacionMaternidad.aspx");
                return;
            }

            Response.Redirect("~/EvaluacionSaludMental.aspx");    
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
