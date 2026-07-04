using System;
using System.Web.UI;

namespace AutoTriageWeb
{
    public partial class EscaneoDocumento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Identificacion.aspx");
        }
    }
}
