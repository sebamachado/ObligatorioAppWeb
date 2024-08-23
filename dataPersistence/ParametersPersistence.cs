using System;
using System.Data;
using System.Data.SqlClient;
using sharedEntities;

namespace dataPersistence
{
    public class ParametersPersistence
    {
        public static string GetParameter(string pName)
        {
            string pValue = string.Empty;

            using (SqlConnection objConnection = new SqlConnection(conection.Cnn))
            {
                using (SqlCommand objCommand = new SqlCommand("BuscarParametro", objConnection))
                {
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@parametro", pName);

                    try
                    {
                        objConnection.Open();
                        using (SqlDataReader objReader = objCommand.ExecuteReader())
                        {
                            if (objReader.Read())
                            {
                                if (!objReader.IsDBNull(objReader.GetOrdinal("Valor")))
                                {
                                    pValue = objReader["Valor"].ToString();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al obtener el parámetro: " + ex.Message);
                    }
                }
            }
            return pValue;
        }
    }
}
