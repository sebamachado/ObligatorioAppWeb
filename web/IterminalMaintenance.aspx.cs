﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using program;
using sharedEntities;

public partial class IterminalMaintenance : System.Web.UI.Page
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
        txt_countryTer.Enabled = false;
        txt_countryTer.Text = "";

        btnSearch.Enabled = true;
        btnCreate.Enabled = false;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
    }


    private void EnableButtons(bool isCreate)
    {
        txt_codTer.Enabled = false;
        txt_cityTer.Enabled = true;
        txt_countryTer.Enabled = true;

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

            InternationalTerminal objIterminal = TerminalActions.ReadI(txt_codTer.Text.Trim());
            if (objIterminal != null)
            {
                EnableButtons(false);
                txt_cityTer.Text = objIterminal.CityName;
                txt_countryTer.Text = objIterminal.Country;
                Session["objIterminal"] = objIterminal;
            }
            else
            {
                EnableButtons(true);
                lblError.ForeColor = Color.Blue;
                lblError.Text = "No existe una Terminal con ese Código";
                Session["objIterminal"] = null;
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
            // Validar parámetros
            if (string.IsNullOrWhiteSpace(txt_codTer.Text.Trim()))
            {
                throw new Exception("Código de la Terminal es de ingreso obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(txt_cityTer.Text.Trim()))
            {
                throw new Exception("Ciudad es de ingreso obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(txt_countryTer.Text.Trim()))
            {
                throw new Exception("Pais es de ingreso obligatorio.");
            }


            InternationalTerminal objIterminal = new InternationalTerminal(txt_codTer.Text.Trim(), txt_cityTer.Text.Trim(), txt_countryTer.Text.Trim());
            TerminalActions.CreateI(objIterminal);

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
            if (string.IsNullOrWhiteSpace(txt_countryTer.Text.Trim()))
            {
                throw new Exception("Pais es de ingreso obligatorio.");
            }

            InternationalTerminal objIterminal = new InternationalTerminal(txt_codTer.Text.Trim(), txt_cityTer.Text.Trim(), txt_countryTer.Text.Trim());
            TerminalActions.UpdateI(objIterminal);

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

            InternationalTerminal objIterminal = new InternationalTerminal(txt_codTer.Text.Trim(), txt_cityTer.Text.Trim(), txt_countryTer.Text.Trim());
            TerminalActions.DeleteI(objIterminal);

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


