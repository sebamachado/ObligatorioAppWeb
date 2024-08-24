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

        PopulateTerminalDropdown();
        PopulateYearDropdown();
        PopulateMonthDropdown();
        ddl_month.Text = "1";
        ddl_year.Text = "1900";

        lblError.Text = "";
        AllTrips();
    }
    private void AllTrips()
    {
        lblError.Text = "";

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

        gvListTripTerminalMY.DataSource = null;
        gvListTripTerminalMY.DataBind();
        string shortTerminalCode = ddl_terminal.Text.Trim().Length > 6 ? ddl_terminal.Text.Trim().Substring(0, 6) : ddl_terminal.Text.Trim();

        try
        {
            if (string.IsNullOrWhiteSpace(ddl_terminal.Text.Trim()) || ddl_terminal.Text.Trim() == "Seleccione")
            {
                throw new Exception("Código de Terminal es de ingreso obligatorio.");
            }
            Terminal arrivalTerminal = TerminalActions.Read(shortTerminalCode);
            if (arrivalTerminal == null)
            {
                throw new Exception("No existe una Terminal con ese Código.");
            }
            if (string.IsNullOrWhiteSpace(ddl_month.Text.Trim()))
            {
                throw new Exception("El Mes es de ingreso obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(ddl_year.Text.Trim()))
            {
                throw new Exception("El Año es de ingreso obligatorio.");
            }
            List<Trip> colTripsTMY = TripActions.ListTripsByTerminalMY(arrivalTerminal, ddl_month.Text.Trim(), ddl_year.Text.Trim());

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
    private void PopulateYearDropdown()
    {
        for (int year = 1900; year <= 2100; year++)
        {
            ddl_year.Items.Add(new ListItem(year.ToString(), year.ToString()));
        }
    }
    private void PopulateMonthDropdown()
    {
        for (int month = 1; month <= 12; month++)
        {
            ddl_month.Items.Add(new ListItem(month.ToString(), month.ToString()));
        }
    }
    private void PopulateTerminalDropdown()
    {
        ddl_terminal.Items.Clear();
        List<NationalTerminal> nationalTerminals = TerminalActions.ListNterminals();
        List<InternationalTerminal> internationalTerminals = TerminalActions.ListIterminals();
        ddl_terminal.Items.Add(new ListItem("Seleccione", ""));

        foreach (var terminal in nationalTerminals)
        {
            ddl_terminal.Items.Add(new ListItem(terminal.Id + " - " + terminal.CityName));
        }

        foreach (var terminal in internationalTerminals)
        {
            ddl_terminal.Items.Add(new ListItem(terminal.Id + " - " + terminal.CityName + ", " + terminal.Country));
        }
    }

}
