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
            // Verificar campos obligatorios
            string departureDateStr = txt_DepartureDate.Text.Trim();
            string estimatedArrivalDateStr = txt_EstimatedArrivalDate.Text.Trim();

            if (string.IsNullOrWhiteSpace(departureDateStr))
            {
                throw new Exception("La fecha y hora de salida es obligatoria.");
            }
            if (string.IsNullOrWhiteSpace(estimatedArrivalDateStr))
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

            // Validar y convertir fechas
            DateTime DepartureDate;
            DateTime EstimatedArrivalDate;
            string format = "dd/MM/yyyy HH:mm:ss";

            if (!DateTime.TryParseExact(departureDateStr, format, null, System.Globalization.DateTimeStyles.None, out DepartureDate))
            {
                throw new Exception("La fecha/hora no tiene el formato correcto. Se espera dd/MM/yyyy HH:mm:ss");
            }

            if (!DateTime.TryParseExact(estimatedArrivalDateStr, format, null, System.Globalization.DateTimeStyles.None, out EstimatedArrivalDate))
            {
                throw new Exception("La fecha/hora no tiene el formato correcto. Se espera dd/MM/yyyy HH:mm:ss");
            }

            // Convertir otros parámetros
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
            Trip objTrip = new Trip(DepartureDate, EstimatedArrivalDate, MaxPassengers, TicketPrice, PlatformNumber, arrivalTerminal, companyTrip);
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
    // metodos auxiliares
    // Método para validar el número de teléfono
    private bool IsValidValue(string value)
    {
        // Valida el formato de número de teléfono: uno o más números separados por punto y coma
        return System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d+(\.\d+)?$");
    }
    private bool IsNumber(string value)
    {
        // Valida el formato de número de teléfono: uno o más números separados por punto y coma
        return System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d+$");
    }
}
