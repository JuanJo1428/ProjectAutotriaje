using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppAutotriajeProject
{
    public partial class Finalizacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            Session.Clear(); // elimina todas las variables de sesión
            Response.Redirect("~/Default.aspx");
        }
    }
}