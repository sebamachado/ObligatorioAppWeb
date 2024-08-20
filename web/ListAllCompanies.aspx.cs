using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using program;
using sharedEntities;
using System.Drawing;

public partial class ListAllCompanies : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";

        gvListCompanies.DataSource = null;
        gvListCompanies.DataBind();

        try
        {
            List<Company> colCompanies = CompanyAction.ListCompanies();

            if (colCompanies.Count > 0)
            {
                gvListCompanies.DataSource = colCompanies; 
                gvListCompanies.DataBind();

                lblError.ForeColor = Color.Blue;
                lblError.Text = "Se encontraron " + colCompanies.Count + " compañías.";
            }
            else
            {
                gvListCompanies.DataSource = null;
                gvListCompanies.DataBind();

                lblError.ForeColor = Color.Red;
                lblError.Text = "No se encontraron compañías.";
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }
}
