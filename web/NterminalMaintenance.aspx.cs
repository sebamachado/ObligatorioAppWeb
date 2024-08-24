using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using program;
using sharedEntities;

public partial class NterminalMaintenance : System.Web.UI.Page
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
        txt_codTer.Enabled = true;
        txt_codTer.Text = "";
        txt_cityTer.Enabled = false;
        txt_cityTer.Text = "";
        rbl_taxiService.ClearSelection();

        btnSearch.Enabled = true;
        btnCreate.Enabled = false;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
    }


    private void EnableButtons(bool isCreate)
    {
        txt_codTer.Enabled = false;
        txt_cityTer.Enabled = true;
        rbl_taxiService.Enabled = true;

        btnSearch.Enabled = false;
        btnCreate.Enabled = isCreate;
        btnUpdate.Enabled = !isCreate;
        btnDelete.Enabled = !isCreate;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        CleanForm();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txt_codTer.Text))
            {
                throw new Exception("Código de Terminal es de ingreso obligatorio.");
            }
            if (!Is6Digits(txt_codTer.Text.Trim()))
            {
                throw new Exception("El Codigo de la terminal debe tener 6 digitos.");
            }

            NationalTerminal objNterminal = TerminalActions.ReadN(txt_codTer.Text.Trim());
            if (objNterminal != null)
            {
                EnableButtons(false);
                txt_cityTer.Text = objNterminal.CityName;

                if (objNterminal.TaxiServiceText == "SI")
                {
                    rbl_taxiService.SelectedValue = "true";
                }
                else
                {
                    rbl_taxiService.SelectedValue = "false";
                }
                Session["objNterminal"] = objNterminal;
            }
            else
            {
                EnableButtons(true);
                lblError.ForeColor = Color.Blue;
                lblError.Text = "No existe una Terminal con ese Código";
                Session["objNterminal"] = null;
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }



    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txt_codTer.Text.Trim()))
            {
                throw new Exception("Código de la Terminal es de ingreso obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(txt_cityTer.Text.Trim()))
            {
                throw new Exception("Ciudad es de ingreso obligatorio.");
            }
            if (string.IsNullOrEmpty(rbl_taxiService.SelectedValue))
            {
                throw new Exception("Seleccione una opción para Servicio Taxi.");
            }

            NationalTerminal objNterminal = new NationalTerminal(txt_codTer.Text.Trim(), txt_cityTer.Text.Trim(), bool.Parse(rbl_taxiService.SelectedValue));
            TerminalActions.CreateI(objNterminal);

            lblError.ForeColor = Color.Blue;
            lblError.Text = "Alta con éxito";
            CleanForm();
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }



    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txt_codTer.Text.Trim()))
            {
                throw new Exception("Código de la Terminal es de ingreso obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(txt_cityTer.Text.Trim()))
            {
                throw new Exception("Ciudad es de ingreso obligatorio.");
            }
            // Validar la opción seleccionada
            if (string.IsNullOrEmpty(rbl_taxiService.SelectedValue))
            {
                throw new Exception("Seleccione una opción para Servicio Taxi.");
            }

            NationalTerminal objNterminal = new NationalTerminal(txt_codTer.Text.Trim(), txt_cityTer.Text.Trim(), bool.Parse(rbl_taxiService.SelectedValue));
            TerminalActions.UpdateN(objNterminal);

            lblError.ForeColor = Color.Blue;
            lblError.Text = "Modificación con éxito";
            CleanForm();
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txt_codTer.Text.Trim()))
            {
                throw new Exception("Código de la Terminal es de ingreso obligatorio.");
            }

            NationalTerminal objNterminal = new NationalTerminal(txt_codTer.Text.Trim(), txt_cityTer.Text.Trim(), bool.Parse(rbl_taxiService.SelectedValue));
            TerminalActions.DeleteN(objNterminal);

            lblError.ForeColor = Color.Blue;
            lblError.Text = "Borrado con éxito";
            CleanForm();
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }
    private bool Is6Digits(string value)
    {
        // Valida el formato de número de teléfono: uno o más números separados por punto y coma
        return System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d{6}$");
    }
}


