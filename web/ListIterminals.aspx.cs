using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using program;
using sharedEntities;
using System.Drawing;
  
public partial class ListIterminals : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        lblError.Text = "";

        gvListIterminals.DataSource = null;
        gvListIterminals.DataBind();

        try
        {
            List<InternationalTerminal> coliTerminals = TerminalActions.ListIterminals();

            if (coliTerminals.Count > 0)
            {
                gvListIterminals.DataSource = coliTerminals;
                gvListIterminals.DataBind();

                lblError.ForeColor = Color.Blue;
                lblError.Text = "Se encontraron " + coliTerminals.Count + " Terminales Internacionales.";
            }
            else
            {
                gvListIterminals.DataSource = null;
                gvListIterminals.DataBind();

                lblError.ForeColor = Color.Red;
                lblError.Text = "No se encontraron Terminales Internacionales.";
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }
}
