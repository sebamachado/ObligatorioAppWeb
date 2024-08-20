using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using program;
using sharedEntities;
using System.Drawing;

public partial class ListNterminals : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";

        gvListNterminals.DataSource = null;
        gvListNterminals.DataBind();

        try
        {
            List<NationalTerminal> coliTerminals = TerminalActions.ListNterminals();

            if (coliTerminals.Count > 0)
            {
                gvListNterminals.DataSource = coliTerminals;
                gvListNterminals.DataBind();

                lblError.ForeColor = Color.Blue;
                lblError.Text = "Se encontraron " + coliTerminals.Count + " Terminales Nacionales.";
            }
            else
            {
                gvListNterminals.DataSource = null;
                gvListNterminals.DataBind();

                lblError.ForeColor = Color.Red;
                lblError.Text = "No se encontraron Terminales Nacionales.";
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }
}
