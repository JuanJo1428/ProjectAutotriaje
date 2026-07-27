using System;
using System.Web.UI;

namespace AppAutotriajeProject
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                litYear.Text = DateTime.Now.Year.ToString();
            }
        }
    }
}
