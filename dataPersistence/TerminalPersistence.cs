using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using sharedEntities;
 
namespace dataPersistence 
{
    public class TerminalPersistence
    {
        public static NationalTerminal findNterminal(string pCodTerm)
        {
            NationalTerminal objNterminal = null;
            string cityTerm;
            bool taxiTerm;

            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("BuscarTerminal", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Parameters.AddWithValue("@codigo_terminal", pCodTerm);
            objCommand.Parameters.AddWithValue("@tipo_terminal", 'N');
            
            SqlParameter objParametro = new SqlParameter("@Error", SqlDbType.Int);
            objParametro.Direction = ParameterDirection.ReturnValue;
            objCommand.Parameters.Add(objParametro);
            try
            {
                objConection.Open();
                SqlDataReader objReader = objCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    if (objReader.Read())
                    {
                        cityTerm = objReader["ciudad"].ToString();

                        // Obtén el valor del servicio de taxi y conviértelo a booleano
                        int servicioTaxiValue = Convert.ToInt32(objReader["servicio_taxi"]);
                        taxiTerm = (servicioTaxiValue == 1);

                        objNterminal = new NationalTerminal(pCodTerm, cityTerm, taxiTerm);
                    }
                }
                objReader.Close();
                int error = Convert.ToInt32(objParametro.Value);

                if (error == -6)
                    throw new Exception("El codigo pertenece a una Terminal Internacional");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConection.Close();
            }
            return objNterminal;
        }

        public static void CreateNterminal(NationalTerminal pNterminal)
        {
            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("AgregarTerminal", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;

            objCommand.Parameters.AddWithValue("@id", pNterminal.Id);
            objCommand.Parameters.AddWithValue("@ciudad", pNterminal.CityName);
            objCommand.Parameters.AddWithValue("@servicio_taxi", pNterminal.TaxiService);
            objCommand.Parameters.AddWithValue("@tipo_terminal", 'N');

            SqlParameter objParametro = new SqlParameter("@Error", SqlDbType.Int);
            objParametro.Direction = ParameterDirection.ReturnValue;
            objCommand.Parameters.Add(objParametro);
            try
            {
                objConection.Open();
                objCommand.ExecuteNonQuery();
                int error = Convert.ToInt32(objParametro.Value);

                if (error == -1)
                    throw new Exception("Ya existe una Terminal con ese codigo");
                else if (error == -2)
                    throw new Exception("El servicio de taxi debe ser proporcionado para terminales nacionales");
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

        public static void UpdateNterminal(NationalTerminal pNterminal)
        {
            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("ModificarTerminal", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;

            objCommand.Parameters.AddWithValue("@id", pNterminal.Id);
            objCommand.Parameters.AddWithValue("@nueva_ciudad", pNterminal.CityName);
            objCommand.Parameters.AddWithValue("@nuevo_servicio_taxi", pNterminal.TaxiService);
            objCommand.Parameters.AddWithValue("@tipo_terminal", 'N');

            SqlParameter objReturn = new SqlParameter("@Error", SqlDbType.Int);
            objReturn.Direction = ParameterDirection.ReturnValue;
            objCommand.Parameters.Add(objReturn);

            try
            {
                objConection.Open();
                objCommand.ExecuteNonQuery();

                int resultado = Convert.ToInt32(objReturn.Value);

                if (resultado == -1)
                    throw new Exception("No existe la Terminal - No se modifica");
                else if (resultado == -2)
                    throw new Exception("El servicio de taxi debe ser proporcionado para terminales nacionales");
                else if (resultado == -2)
                    throw new Exception("Ocurrio un error inesperado");
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

        public static void DeleteNterminal(NationalTerminal pNterminal)
        {
            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("EliminarTerminal", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;

            objCommand.Parameters.AddWithValue("@codigo_terminal", pNterminal.Id);
            SqlParameter objReturn = new SqlParameter("@Error", SqlDbType.Int);
            objReturn.Direction = ParameterDirection.ReturnValue;
            objCommand.Parameters.Add(objReturn);

            try
            {
                objConection.Open();
                objCommand.ExecuteNonQuery();
                int resultado = Convert.ToInt32(objReturn.Value);

                if (resultado == -1)
                    throw new Exception("La compañía no existe - No se elimina");

                else if (resultado == -2)
                    throw new Exception("La compañía tiene viajes asociados - No se elimina");

                else if (resultado == -3)
                    throw new Exception("Ocurrio un error inesperado");

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
        public static List<NationalTerminal> ListNterminals()
        {
            List<NationalTerminal> colnTerminals = new List<NationalTerminal>();

            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("ListarTerminales", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Parameters.AddWithValue("@tipo_terminal", 'N');

            try
            {
                objConection.Open();
                SqlDataReader objReader = objCommand.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        string id = objReader["id"].ToString();
                        string city = objReader["ciudad"].ToString();
                        bool taxi_service = objReader["servicio_taxi"].ToString() == "1";

                        NationalTerminal objiTerminal = new NationalTerminal(id, city, taxi_service); 

                        colnTerminals.Add(objiTerminal);
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

            return colnTerminals;
        }

        public static InternationalTerminal findIterminal(string pCodTerm)
        {
            InternationalTerminal objIterminal = null;
            string cityTerm;
            string countryTerm;

            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("BuscarTerminal", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Parameters.AddWithValue("@codigo_terminal", pCodTerm);
            objCommand.Parameters.AddWithValue("@tipo_terminal", 'I');

            SqlParameter objParametro = new SqlParameter("@Error", SqlDbType.Int);
            objParametro.Direction = ParameterDirection.ReturnValue;
            objCommand.Parameters.Add(objParametro);
            try
            {
                objConection.Open();
                SqlDataReader objReader = objCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    if (objReader.Read())
                    {
                        cityTerm = objReader["ciudad"].ToString();
                        countryTerm = objReader["pais"].ToString();

                        objIterminal = new InternationalTerminal(pCodTerm, cityTerm, countryTerm);
                    }
                }
                objReader.Close();
                int error = Convert.ToInt32(objParametro.Value);

                if (error == -6)
                    throw new Exception("El codigo pertenece a una Terminal Nacional");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConection.Close();
            }
            return objIterminal;
        }

        public static void CreateIterminal(InternationalTerminal pIterminal)
        {
            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("AgregarTerminal", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;

            objCommand.Parameters.AddWithValue("@id", pIterminal.Id);
            objCommand.Parameters.AddWithValue("@ciudad", pIterminal.CityName);
            objCommand.Parameters.AddWithValue("@pais", pIterminal.Country);
            objCommand.Parameters.AddWithValue("@tipo_terminal", 'I');

            SqlParameter objParametro = new SqlParameter("@Error", SqlDbType.Int);
            objParametro.Direction = ParameterDirection.ReturnValue;
            objCommand.Parameters.Add(objParametro);
            try
            {
                objConection.Open();
                objCommand.ExecuteNonQuery();
                int error = Convert.ToInt32(objParametro.Value);

                if (error == -1)
                    throw new Exception("Ya existe una Terminal con ese codigo");
                else if (error == -2)
                    throw new Exception("El pais debe ser proporcionado para terminales Internacionales");
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

        public static void UpdateIterminal(InternationalTerminal pIterminal)
        {
            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("ModificarTerminal", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;

            objCommand.Parameters.AddWithValue("@id", pIterminal.Id);
            objCommand.Parameters.AddWithValue("@nueva_ciudad", pIterminal.CityName);
            objCommand.Parameters.AddWithValue("@nuevo_pais", pIterminal.Country);
            objCommand.Parameters.AddWithValue("@tipo_terminal", 'I');

            SqlParameter objReturn = new SqlParameter("@Error", SqlDbType.Int);
            objReturn.Direction = ParameterDirection.ReturnValue;
            objCommand.Parameters.Add(objReturn);

            try
            {
                objConection.Open();
                objCommand.ExecuteNonQuery();

                int resultado = Convert.ToInt32(objReturn.Value);

                if (resultado == -1)
                    throw new Exception("No existe la Terminal - No se modifica");
                else if (resultado == -2)
                    throw new Exception("El servicio de taxi debe ser proporcionado para terminales nacionales");
                else if (resultado == -2)
                    throw new Exception("Ocurrio un error inesperado");
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

        public static void DeleteIterminal(InternationalTerminal pIterminal)
        {
            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("EliminarTerminal", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;

            objCommand.Parameters.AddWithValue("@codigo_terminal", pIterminal.Id);
            SqlParameter objReturn = new SqlParameter("@Error", SqlDbType.Int);
            objReturn.Direction = ParameterDirection.ReturnValue;
            objCommand.Parameters.Add(objReturn);

            try
            {
                objConection.Open();
                objCommand.ExecuteNonQuery();
                int resultado = Convert.ToInt32(objReturn.Value);

                if (resultado == -1)
                    throw new Exception("La compañía no existe - No se elimina");

                else if (resultado == -2)
                    throw new Exception("La compañía tiene viajes asociados - No se elimina");

                else if (resultado == -3)
                    throw new Exception("Ocurrio un error inesperado");

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
        public static List<InternationalTerminal> ListIterminals()
        {
            List<InternationalTerminal> coliTerminals = new List<InternationalTerminal>();

            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("ListarTerminales", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Parameters.AddWithValue("@tipo_terminal", 'I');

            try
            {
                objConection.Open();
                SqlDataReader objReader = objCommand.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        string id = objReader["id"].ToString();
                        string city = objReader["ciudad"].ToString();
                        string country = objReader["pais"].ToString();

                        InternationalTerminal objiTerminal = new InternationalTerminal(id, city, country);

                        coliTerminals.Add(objiTerminal);
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

            return coliTerminals;
        }
    }
}
