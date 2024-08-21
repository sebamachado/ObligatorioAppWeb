using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using sharedEntities;

namespace dataPersistence
{
    public class TripPersistence
    {

        public static void AddTrip(Trip pTrip)
        {
            Company comp=pTrip.CompanyTrip;
            Terminal term = pTrip.ArrivalTerminal;

            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("AgregarViaje", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;

            objCommand.Parameters.AddWithValue("@dt_salida", pTrip.DepartureDate);
            objCommand.Parameters.AddWithValue("@dt_llegada", pTrip.EstimatedArrivalDate);
            objCommand.Parameters.AddWithValue("@max_pasajeros", pTrip.MaxPassengers);
            objCommand.Parameters.AddWithValue("@precio_boleto", pTrip.TicketPrice);
            objCommand.Parameters.AddWithValue("@num_anden", pTrip.PlatformNumber);
            objCommand.Parameters.AddWithValue("@nombre_compania", comp.Name);
            objCommand.Parameters.AddWithValue("@id_terminal", term.Id);

            SqlParameter objParametro = new SqlParameter("@Error", SqlDbType.Int);
            objParametro.Direction = ParameterDirection.ReturnValue;
            objCommand.Parameters.Add(objParametro);
            try
            {
                objConection.Open();
                objCommand.ExecuteNonQuery();
                int error = Convert.ToInt32(objParametro.Value);

                if (error == -1)
                    throw new Exception("No existe una Compañía con ese codigo");
                else if (error == -2)
                    throw new Exception("No existe una Terminal con ese codigo");
                else if (error == -3)
                    throw new Exception("Ocurrio un Error Inesperado");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                objConection.Close();
            }
        }
    }
}
