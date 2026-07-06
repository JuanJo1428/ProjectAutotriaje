using ProjectDto.Dtos.EscanerDtos;
using ProjectServices.Constants;
using System;
using System.Web.UI;

namespace AutoTriageWeb
{
    public partial class EscaneoDocumento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEscaneoExitoso_Click(object sender, EventArgs e)
        {
            PacienteEscaneadoDto paciente = SimularRespuestaEscaner();

            ProcesarEscaneoExitoso(paciente);
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Identificacion.aspx");
        }

        private void ProcesarEscaneoExitoso(PacienteEscaneadoDto pacienteEscaneado)
        {
            Session["PacienteEscaneado"] = pacienteEscaneado;

            Session["FlujoEscaner"] = true;

            Response.Redirect("~/InformacionPaciente.aspx");
        }

        private void OnDocumentoEscaneado(PacienteEscaneadoDto paciente)
        {
            ProcesarEscaneoExitoso(paciente);
        }


        //Simulacion Escaneo
        private PacienteEscaneadoDto SimularRespuestaEscaner()
        {
            return new PacienteEscaneadoDto
            {
                DescripcionTipoDocumento = "Cédula de Ciudadanía",
                IdTipoDocumento = 1,

                NroDocumento = "123456789",

                PrimerNombre = "JUAN",
                SegundoNombre = "CARLOS",

                PrimerApellido = "PEREZ",
                SegundoApellido = "GOMEZ",

                IdGenero = (int)Generos.Masculino,

                FechaNacimiento = new DateTime(1998, 5, 18),

                LugarNacimiento = "MEDELLIN"
            };
        }
    }
}
