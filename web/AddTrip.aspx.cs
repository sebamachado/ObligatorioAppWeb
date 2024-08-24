using System;
using System.Drawing;
using System.Text.RegularExpressions;
using program;
using sharedEntities;

public partial class AddTrip : System.Web.UI.Page
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
        txt_DepartureDate.Attributes["type"] = "datetime-local";
        txt_EstimatedArrivalDate.Attributes["type"] = "datetime-local";
        txt_DepartureDate.Text = "";
        txt_EstimatedArrivalDate.Text = "";
        txt_MaxPassengers.Text = "";
        txt_TicketPrice.Text = "";
        txt_PlatformNumber.Text = "";
        txt_CompanyTrip.Text = "";
        txt_ArrivalTerminal.Text = "";

        lblError.Text = "";
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        CleanForm();
    }

    protected void btnAddTrip_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txt_DepartureDate.Text.Trim()))
            {
                throw new Exception("La fecha y hora de salida es obligatoria.");
            }
            if (string.IsNullOrWhiteSpace(txt_EstimatedArrivalDate.Text.Trim()))
            {
                throw new Exception("La fecha y hora aproximada de llegada es obligatoria.");
            }
            if (string.IsNullOrWhiteSpace(txt_MaxPassengers.Text.Trim()))
            {
                throw new Exception("El máximo de pasajeros es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(txt_TicketPrice.Text.Trim()))
            {
                throw new Exception("El precio del boleto es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(txt_PlatformNumber.Text.Trim()))
            {
                throw new Exception("El número de andén es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(txt_CompanyTrip.Text.Trim()))
            {
                throw new Exception("El nombre de la compañía es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(txt_ArrivalTerminal.Text.Trim()))
            {
                throw new Exception("El nombre de la terminal de destino es obligatorio.");
            }
            if (!IsValidValue(txt_TicketPrice.Text.Trim()))
            {
                throw new Exception("El Precio ingresado no tiene un formato valido.");
            }
            if (!IsValidValue(txt_MaxPassengers.Text.Trim()))
            {
                throw new Exception("Máximo de Pasajeros solo acepta digitos.");
            }
            if (!IsValidValue(txt_PlatformNumber.Text.Trim()))
            {
                throw new Exception("Número de Andén solo acepta digitos.");
            }

            DateTime departureDate = DateTime.ParseExact(txt_DepartureDate.Text.Trim(), "yyyy-MM-ddTHH:mm", null);
            DateTime estimatedArrivalDate = DateTime.ParseExact(txt_EstimatedArrivalDate.Text.Trim(), "yyyy-MM-ddTHH:mm", null);

            int MaxPassengers = int.Parse(txt_MaxPassengers.Text.Trim());
            double TicketPrice = double.Parse(txt_TicketPrice.Text.Trim());
            int PlatformNumber = int.Parse(txt_PlatformNumber.Text.Trim());
            string companyTrip_str = txt_CompanyTrip.Text.Trim();
            string arrivalTerminal_str = txt_ArrivalTerminal.Text.Trim();

            // Validar existencia de la terminal y la compañía
            Terminal arrivalTerminal = TerminalActions.Read(arrivalTerminal_str);
            if (arrivalTerminal == null)
            {
                throw new Exception("No existe una Terminal con ese Código.");
            }

            Company companyTrip = CompanyAction.Read(companyTrip_str);
            if (companyTrip == null)
            {
                throw new Exception("No existe una Compañía con ese Nombre.");
            }

            // Crear el objeto Trip
            Trip objTrip = new Trip(departureDate, estimatedArrivalDate, MaxPassengers, TicketPrice, PlatformNumber, arrivalTerminal, companyTrip);
            TripActions.AddTrip(objTrip);

            lblError.ForeColor = Color.Blue;
            lblError.Text = "Viaje agregado con éxito";
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }

    private bool IsValidValue(string value)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d+(\.\d+)?$");
    }

    private bool IsNumber(string value)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d+$");
    }
}
