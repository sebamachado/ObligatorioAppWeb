using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;
using program;
using sharedEntities;

public partial class CompanyMaintenance : System.Web.UI.Page
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
        txt_nomComp.Enabled = true;
        txt_nomComp.Text = "";
        txt_dirComp.Enabled = false;
        txt_dirComp.Text = "";
        txt_telComp.Enabled = false;
        txt_telComp.Text = "";

        btnSearch.Enabled = true;
        btnCreate.Enabled = false;
        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
    }


    private void EnableButtons(bool isCreate)
    {
        txt_nomComp.Enabled = false;
        txt_dirComp.Enabled = true;
        txt_telComp.Enabled = true;

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
            if (txt_nomComp.Text == "")
                throw new Exception("Nombre de la Compañia es de ingreso obligatorio.");

            string compName = txt_nomComp.Text.Trim();

            Company objCompany = CompanyAction.Read(compName);

            if (objCompany != null)
            {
                EnableButtons(false);
                txt_dirComp.Text = objCompany.MatrizAddress; 
                txt_telComp.Text = objCompany.ContactPhone;

                Session["objCompany"] = objCompany;
            }
            else
            {
                EnableButtons(true);
                lblError.ForeColor = Color.Blue;
                lblError.Text = "No existe una Compañia con ese nombre";

                Session["objCompany"] = null;
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
            string compName = txt_nomComp.Text.Trim();
            string compDir = txt_dirComp.Text.Trim();
            string compTel = txt_telComp.Text.Trim();

            // Validar parametros
            if (string.IsNullOrWhiteSpace(compName))
            {
                throw new Exception("Nombre de la Compañia es de ingreso obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(compDir))
            {
                throw new Exception("Direccion de la Compañia es de ingreso obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(compTel))
            {
                throw new Exception("Número de teléfono es de ingreso obligatorio.");
            }
            if (!IsValidPhoneNumber(compTel))
            {
                throw new Exception("Por favor, ingrese uno o más números de teléfono válidos, separados por punto y coma. Ejemplos: 7685855 o 64567567;745745747. Asegúrese de que no haya un punto y coma al final de la cadena.");
            }

            Company objCompany = new Company(compName, compDir, compTel); 

            CompanyAction.Create(objCompany);

            lblError.ForeColor = Color.Blue;
            lblError.Text = "Alta con exito";

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
            string compName = txt_nomComp.Text.Trim();
            string compDir = txt_dirComp.Text.Trim();
            string compTel = txt_telComp.Text.Trim();

            // Validar parametros
            if (string.IsNullOrWhiteSpace(compName))
            {
                throw new Exception("Nombre de la Compañia es de ingreso obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(compDir))
            {
                throw new Exception("Direccion de la Compañia es de ingreso obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(compTel))
            {
                throw new Exception("Número de teléfono es de ingreso obligatorio.");
            }
            if (!IsValidPhoneNumber(compTel))
            {
                throw new Exception("Por favor, ingrese uno o más números de teléfono válidos, separados por punto y coma. Ejemplos: 7685855 o 64567567;745745747. Asegúrese de que no haya un punto y coma al final de la cadena.");
            }

            Company objCompany = new Company(compName, compDir, compTel);

            CompanyAction.Update(objCompany);

            lblError.ForeColor = Color.Blue;
            lblError.Text = "Modificacion con exito";

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
            string compName = txt_nomComp.Text.Trim();
            string compDir = txt_dirComp.Text.Trim();
            string compTel = txt_telComp.Text.Trim();

            // Validar parametros
            if (string.IsNullOrWhiteSpace(compName))
            {
                throw new Exception("Nombre de la Compañía es de ingreso obligatorio.");
            }

            Company objCompany = new Company(compName, compDir, compTel);

            CompanyAction.Delete(objCompany);

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


// metodos auxiliares
    // Método para validar el número de teléfono
    private bool IsValidPhoneNumber(string phoneNumber)
    {
        // Valida el formato de número de teléfono: uno o más números separados por punto y coma
        return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\d+(;\d+)*$");
    }
}


