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

        public static List<Trip> ListTripsByTerminalMY(Terminal pTerminal, string pMonth, string pYear)
        {
            List<Trip> colTrips = new List<Trip>();

            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("ListadoViajesTerminalMesAño", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Parameters.AddWithValue("@id_terminal", pTerminal.Id);
            objCommand.Parameters.AddWithValue("@mes", pMonth);
            objCommand.Parameters.AddWithValue("@anio", pYear);

            try
            {
                objConection.Open();
                SqlDataReader objReader = objCommand.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        DateTime DepartureDate = DateTime.Parse(objReader["dt_salida"].ToString());
                        DateTime EstimatedArrivalDate = DateTime.Parse(objReader["dt_llegada"].ToString());
                        int MaxPassengers = int.Parse(objReader["max_pasajeros"].ToString());
                        double TicketPrice = double.Parse(objReader["precio_boleto"].ToString());
                        int PlatformNumber = int.Parse(objReader["num_anden"].ToString());
                        string nombre_compania = objReader["nombre_compania"].ToString();
                        string term_code = objReader["id_terminal"].ToString();
                        Company pCompany = CompanyPersistence.findCompany(nombre_compania);

                        Trip objTrip = new Trip(DepartureDate, EstimatedArrivalDate, MaxPassengers, TicketPrice, PlatformNumber,pTerminal,pCompany);

                        colTrips.Add(objTrip);
                    }
                }

                objReader.Close();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            finally
            {
                objConection.Close();
            }

            return colTrips;
        }
        public static List<Trip> ListAllTrips()
        {
            List<Trip> colTrips = new List<Trip>();

            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("ListadoViajes", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                objConection.Open();
                SqlDataReader objReader = objCommand.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        DateTime DepartureDate = DateTime.Parse(objReader["dt_salida"].ToString());
                        DateTime EstimatedArrivalDate = DateTime.Parse(objReader["dt_llegada"].ToString());
                        int MaxPassengers = int.Parse(objReader["max_pasajeros"].ToString());
                        double TicketPrice = double.Parse(objReader["precio_boleto"].ToString());
                        int PlatformNumber = int.Parse(objReader["num_anden"].ToString());
                        string nombre_compania = objReader["nombre_compania"].ToString();
                        string term_code = objReader["id_terminal"].ToString();
                        Company pCompany = CompanyPersistence.findCompany(nombre_compania);
                        Terminal pTerminal = TerminalPersistence.FindTerminal(term_code);

                        Trip objTrip = new Trip(DepartureDate, EstimatedArrivalDate, MaxPassengers, TicketPrice, PlatformNumber, pTerminal, pCompany);

                        colTrips.Add(objTrip);
                    }
                }

                objReader.Close();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            finally
            {
                objConection.Close();
            }

            return colTrips;
        }
    }
}
