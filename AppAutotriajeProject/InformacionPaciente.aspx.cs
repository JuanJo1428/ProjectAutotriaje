using ProjectDto.Dtos;
using ProjectDto.Dtos.EscanerDtos;
using ProjectDto.Dtos.RegistroAtencionDtos;
using ProjectCommon.Constants;
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
                QuitarPlaceholders();

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

            QuitarPlaceholders();

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


            // Sincroniza la información del paciente en la sesión correspondiente.
            if (vieneDeEscaner)
            {
                ActualizarSesionEscaner(pacienteProcesado.Paciente);
            }
            else
            {
                ActualizarSesionBusqueda(pacienteProcesado.Paciente);
            }


            //Consulta Registro Pendiente
            ConsultarRegistroPendienteRespuestaDto registroPendiente = _registroService.ConsultarRegistroPendiente(pacienteProcesado);

            if (registroPendiente.TieneRegistroPendiente)
            {
                Session["RegistroPendiente"] = registroPendiente;

                if (registroPendiente.RegistroAtencion.AutotriajeIniciado)
                {
                    Response.Redirect("~/RegistroActivo.aspx");

                }
                Response.Redirect("~/RegistroPendiente.aspx");
                return;
            }


            if (pacienteProcesado.Paciente.IdGenero == (int)Generos.Masculino)
            {

                EvaluacionesPacienteDto evaluaciones = new EvaluacionesPacienteDto
                {
                    CondicionMaternidad = false
                };

                Session["EvaluacionesPaciente"] = evaluaciones;

                Response.Redirect("~/EvaluacionSaludMental.aspx");
                return;
            }

            Response.Redirect("~/EvaluacionMaternidad.aspx");
            return;
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
            object flujo = Session["FlujoEscaner"];

            if (flujo == null)
                return false;

            return (bool)flujo;
        }

        private PacienteValidadoDto ConstruirPacienteValidadoManual()
        {
            BuscarPacienteRespuestaDto respuestaBusqueda = Session["BusquedaPaciente"] as BuscarPacienteRespuestaDto;

            if (respuestaBusqueda == null)
            {
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
            };

        }

        private PacienteValidadoDto ConstruirPacienteValidadoEscaner()
        {
            PacienteEscaneadoDto paciente = Session["PacienteEscaneado"] as PacienteEscaneadoDto;

            if (paciente == null)
            {
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
            };
        }

        private void ActualizarSesionBusqueda(PacienteDto paciente)
        {
            BuscarPacienteRespuestaDto respuestaBusqueda =
                Session["BusquedaPaciente"] as BuscarPacienteRespuestaDto;

            if (respuestaBusqueda == null)
            {
                return;
            }

            respuestaBusqueda.PacientePrincipal = paciente;

            respuestaBusqueda.Existe = true;

            respuestaBusqueda.EncontradoAutotriaje = true;

            Session["BusquedaPaciente"] = respuestaBusqueda;
        }


        private void ActualizarSesionEscaner(PacienteDto paciente)
        {
            PacienteEscaneadoDto pacienteEscaneado =
                Session["PacienteEscaneado"] as PacienteEscaneadoDto;

            if (pacienteEscaneado == null)
            {
                return;
            }

            pacienteEscaneado.PrimerNombre = paciente.PrimerNombre;
            pacienteEscaneado.SegundoNombre = paciente.SegundoNombre;

            pacienteEscaneado.PrimerApellido = paciente.PrimerApellido;
            pacienteEscaneado.SegundoApellido = paciente.SegundoApellido;

            pacienteEscaneado.IdGenero = paciente.IdGenero;

            pacienteEscaneado.FechaNacimiento = paciente.FechaNacimiento;


            Session["PacienteEscaneado"] = pacienteEscaneado;
        }

        private void QuitarPlaceholders()
        {
            txtPrimerNombre.Attributes.Remove("placeholder");
            txtSegundoNombre.Attributes.Remove("placeholder");
            txtPrimerApellido.Attributes.Remove("placeholder");
            txtSegundoApellido.Attributes.Remove("placeholder");
        }

    }
}
