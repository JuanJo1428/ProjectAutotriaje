using ProjectDto.Dtos.EscanerDtos;
using ProjectServices.Constants;
using ProjectServices.Implementations;
using System;
using System.Web.UI;

namespace AppAutotriajeProject
{
    public partial class EscaneoDocumento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private readonly EscanerService _escanerService = new EscanerService();
        protected void btnEscaneoExitoso_Click(object sender, EventArgs e)
        {

            string lectura = hdLectura.Value;

            if (string.IsNullOrWhiteSpace(lectura))
            {
                return;
            }

            PacienteEscaneadoDto paciente = _escanerService.ProcesarLectura(lectura);

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

            return;
        }


    }
}