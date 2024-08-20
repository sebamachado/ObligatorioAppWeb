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
                throw new Exception("Código de Terminal es de ingreso obligatorio.");
             
            string codTerm = txt_codTer.Text.Trim();

            NationalTerminal objNterminal = TerminalActions.ReadN(codTerm);

            if (objNterminal != null)
            {
                EnableButtons(false);
                txt_cityTer.Text = objNterminal.CityName;

                // Asigna el valor correcto al RadioButtonList
                if (objNterminal.TaxiServiceText == "SI")
                {
                    rbl_taxiService.SelectedValue = "true"; // Selecciona "SI"
                }
                else
                {
                    rbl_taxiService.SelectedValue = "false"; // Selecciona "NO"
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
            string codTerm = txt_codTer.Text.Trim();
            string cityTer = txt_cityTer.Text.Trim();
            string selectedValue = rbl_taxiService.SelectedValue;

            // Validar parámetros
            if (string.IsNullOrWhiteSpace(codTerm))
            {
                throw new Exception("Código de la Terminal es de ingreso obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(cityTer))
            {
                throw new Exception("Ciudad es de ingreso obligatorio.");
            }
            // Validar la opción seleccionada
            if (string.IsNullOrEmpty(selectedValue))
            {
                throw new Exception("Seleccione una opción para Servicio Taxi.");
            }

            bool taxiService = bool.Parse(selectedValue);

            NationalTerminal objNterminal = new NationalTerminal(codTerm, cityTer, taxiService);

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
            string codTerm = txt_codTer.Text.Trim();
            string cityTer = txt_cityTer.Text.Trim();
            string selectedValue = rbl_taxiService.SelectedValue;

            // Validar parámetros
            if (string.IsNullOrWhiteSpace(codTerm))
            {
                throw new Exception("Código de la Terminal es de ingreso obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(cityTer))
            {
                throw new Exception("Ciudad es de ingreso obligatorio.");
            }
            // Validar la opción seleccionada
            if (string.IsNullOrEmpty(selectedValue))
            {
                throw new Exception("Seleccione una opción para Servicio Taxi.");
            }

            bool taxiService = bool.Parse(selectedValue);

            NationalTerminal objNterminal = new NationalTerminal(codTerm, cityTer, taxiService);

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
            string codTerm = txt_codTer.Text.Trim();
            string cityTer = txt_cityTer.Text.Trim();
            string selectedValue = rbl_taxiService.SelectedValue;

            // Validar parámetros
            if (string.IsNullOrWhiteSpace(codTerm))
            {
                throw new Exception("Código de la Terminal es de ingreso obligatorio.");
            }
            bool taxiService = bool.Parse(selectedValue);

            NationalTerminal objNterminal = new NationalTerminal(codTerm, cityTer, taxiService);

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

}


