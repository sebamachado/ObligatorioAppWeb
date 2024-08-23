using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using program;
using sharedEntities;
using System.Drawing;

public partial class ListTripsByTerminalMY : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";
        if (!IsPostBack)
        {
            CleanForm();
        }
    }

    private void CleanForm()
    {
        gvListTripTerminalMY.DataSource = null;
        gvListTripTerminalMY.DataBind();

        txt_terminalCode.Text = "";
        txt_month.Text = "";
        txt_year.Text = "";

        lblError.Text = "";
        AllTrips();
    }
    private void AllTrips()
    {
        lblError.Text = "";
        //btnSearch.Enabled = true;

        gvListTripTerminalMY.DataSource = null;
        gvListTripTerminalMY.DataBind();
        try
        {
            List<Trip> colTripsTMY = TripActions.ListAllTrips();

            if (colTripsTMY.Count > 0)
            {
                gvListTripTerminalMY.DataSource = colTripsTMY;
                gvListTripTerminalMY.DataBind();

                lblError.ForeColor = Color.Blue;
                lblError.Text = "Se encontraron " + colTripsTMY.Count + " Viajes.";
            }
            else
            {
                gvListTripTerminalMY.DataSource = null;
                gvListTripTerminalMY.DataBind();

                lblError.ForeColor = Color.Red;
                lblError.Text = "No se encontraron Viajes.";
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        CleanForm();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        //btnSearch.Enabled = true;

        gvListTripTerminalMY.DataSource = null;
        gvListTripTerminalMY.DataBind();
        string terminalCode = txt_terminalCode.Text.Trim();
        string month = txt_month.Text.Trim();
        string year = txt_year.Text.Trim();

        try
        {
            Terminal arrivalTerminal = TerminalActions.Read(terminalCode);
            if (arrivalTerminal == null)
            {
                throw new Exception("No existe una Terminal con ese Código.");
            }
            List<Trip> colTripsTMY = TripActions.ListTripsByTerminalMY(arrivalTerminal, month, year);

            if (colTripsTMY.Count > 0)
            {
                gvListTripTerminalMY.DataSource = colTripsTMY;
                gvListTripTerminalMY.DataBind();

                lblError.ForeColor = Color.Blue;
                lblError.Text = "Se encontraron " + colTripsTMY.Count + " Viajes.";
            }
            else
            {
                gvListTripTerminalMY.DataSource = null;
                gvListTripTerminalMY.DataBind();

                lblError.ForeColor = Color.Red;
                lblError.Text = "No se encontraron Viajes.";
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }
}
