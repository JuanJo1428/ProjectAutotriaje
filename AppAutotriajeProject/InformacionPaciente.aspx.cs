using ProjectDto.Dtos;
using ProjectDto.Dtos.EscanerDtos;
using ProjectDto.Dtos.RegistroAtencionDtos;
using ProjectServices.Constants;
using ProjectServices.Implementations;
using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAutotriajeProject
{
    public partial class InformacionPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGeneros();

                if (VieneDeEscaner())
                {
                    CargarPacienteEscaneado();
                }
                else
                {
                    CargarPacienteManual();
                }
            }
        }


        private void CargarPacienteManual()
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


        private void CargarPacienteEscaneado()
        {
            PacienteEscaneadoDto paciente = Session["PacienteEscaneado"] as PacienteEscaneadoDto;

            if (paciente == null)
            {
                Response.Redirect("~/Identificacion.aspx");
                return;
            }

            txtTipoDocumento.Text = paciente.DescripcionTipoDocumento;
            txtNumeroDocumento.Text = paciente.NroDocumento;

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

            bool vieneDeEscaner = VieneDeEscaner();

            PacienteValidadoDto pacienteValidado;

            if (vieneDeEscaner)
            {
                pacienteValidado = ConstruirPacienteValidadoEscaner();
            }
            else
            {
                pacienteValidado = ConstruirPacienteValidadoManual();
            }


            if (pacienteValidado == null)
            {
                Response.Redirect("~/Identificacion.aspx");
                return;
            }
        

            //Procesa Paciente
            PacienteProcesadoRespuestaDto pacienteProcesado = _pacienteService.ProcesarPaciente(pacienteValidado);

            Session["PacienteProcesado"] = pacienteProcesado;


            // Actualiza el paciente guardado en la session busqueda cuando el flujo es manual
            if (!vieneDeEscaner)
            {
                BuscarPacienteRespuestaDto respuestaBusqueda = Session["BusquedaPaciente"] as BuscarPacienteRespuestaDto;

                if (respuestaBusqueda != null)
                {
                    respuestaBusqueda.PacientePrincipal = pacienteProcesado.Paciente;
                    respuestaBusqueda.Existe = true;
                    respuestaBusqueda.EncontradoAutotriaje = true;

                    Session["BusquedaPaciente"] = respuestaBusqueda;
                }
            }


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

        private bool VieneDeEscaner()
        {
            return Session["PacienteEscaneado"] != null;
        }

        private PacienteValidadoDto ConstruirPacienteValidadoManual()
        {
            BuscarPacienteRespuestaDto respuestaBusqueda = Session["BusquedaPaciente"] as BuscarPacienteRespuestaDto;

            if (respuestaBusqueda == null)
            {
                Response.Redirect("~/Identificacion.aspx");
                return null;
            }

            PacienteDto paciente = respuestaBusqueda.PacientePrincipal;

            return new PacienteValidadoDto
            {
                IdTipoDocumento = paciente.IdTipoDocumento,

                NroDocumento = paciente.NroDocumento,

                PrimerNombre = txtPrimerNombre.Text.Trim(),

                SegundoNombre = txtSegundoNombre.Text.Trim(),

                PrimerApellido = txtPrimerApellido.Text.Trim(),

                SegundoApellido = txtSegundoApellido.Text.Trim(),

                IdGenero = Convert.ToInt32(ddlSexoBiologico.SelectedValue),

                FechaNacimiento = DateTime.ParseExact(txtFechaNacimiento.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture),

                LugarNacimiento = null
            };

        }

        private PacienteValidadoDto ConstruirPacienteValidadoEscaner()
        {
            PacienteEscaneadoDto paciente = Session["PacienteEscaneado"] as PacienteEscaneadoDto;

            if (paciente == null)
            {
                Response.Redirect("~/Identificacion.aspx");
                return null;
            }

            return new PacienteValidadoDto
            {
                IdTipoDocumento = paciente.IdTipoDocumento,

                NroDocumento = paciente.NroDocumento,

                PrimerNombre = txtPrimerNombre.Text.Trim(),

                SegundoNombre = txtSegundoNombre.Text.Trim(),

                PrimerApellido = txtPrimerApellido.Text.Trim(),

                SegundoApellido = txtSegundoApellido.Text.Trim(),

                IdGenero = Convert.ToInt32(ddlSexoBiologico.SelectedValue),

                FechaNacimiento = DateTime.ParseExact(txtFechaNacimiento.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture),

                LugarNacimiento = paciente.LugarNacimiento
            };
        }



    }
}
