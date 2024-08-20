using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using sharedEntities;

namespace dataPersistence
{
    public class CompanyPersistence
    {
        public static Company findCompany(string pNameComp)
        {
            Company objCompany = null;
            string dirComp;
            string telComp;

            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("BuscarCompania", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Parameters.AddWithValue("@nombre_compania", pNameComp);

            try
            {
                objConection.Open();
                SqlDataReader objReader = objCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    if (objReader.Read())
                    {
                        dirComp = objReader["direccion_matriz"].ToString();
                        telComp = objReader["telefonos"].ToString();

                        objCompany = new Company(pNameComp, dirComp, telComp);
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
            return objCompany;
        }

         
        public static void CreateCompany(Company pCompany)
        {
            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("AgregarCompania", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;

            objCommand.Parameters.AddWithValue("@nombre_compania", pCompany.Name);
            objCommand.Parameters.AddWithValue("@direccion_matriz", pCompany.MatrizAddress);
            objCommand.Parameters.AddWithValue("@telefonos", pCompany.ContactPhone);

            SqlParameter objParametro = new SqlParameter("@Error", SqlDbType.Int);
            objParametro.Direction = ParameterDirection.ReturnValue;
            objCommand.Parameters.Add(objParametro);
            try
            {
                objConection.Open();
                objCommand.ExecuteNonQuery();
                int error = Convert.ToInt32(objParametro.Value);

                if (error == -1)
                    throw new Exception("Ya existe una compañía con ese nombre");

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

        public static void UpdateCompany(Company pCompany)
        {
            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("ModificarCompania", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;

            objCommand.Parameters.AddWithValue("@nombre_compania", pCompany.Name);
            objCommand.Parameters.AddWithValue("@direccion_matriz", pCompany.MatrizAddress);
            objCommand.Parameters.AddWithValue("@telefonos", pCompany.ContactPhone);

            SqlParameter objReturn = new SqlParameter("@Error", SqlDbType.Int);
            objReturn.Direction = ParameterDirection.ReturnValue;
            objCommand.Parameters.Add(objReturn);

            try
            {
                objConection.Open();
                objCommand.ExecuteNonQuery();

                int resultado = Convert.ToInt32(objReturn.Value);

                if (resultado == -1)
                    throw new Exception("No existe la Compañía - No se modifica");

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

        public static void DeleteCompany(Company pCompany)
        {
            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("EliminarCompania", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;

            objCommand.Parameters.AddWithValue("@nombre_compania", pCompany.Name);
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

        public static List<Company> ListAllCompanies()
        {
            List<Company> colCompanys = new List<Company>();

            SqlConnection objConection = new SqlConnection(conection.Cnn);
            SqlCommand objCommand = new SqlCommand("ListarCompanias", objConection);
            objCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                objConection.Open();
                SqlDataReader objReader = objCommand.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        string nameComp = objReader["nombre"].ToString();
                        string dirComp = objReader["direccion_matriz"].ToString();
                        string TelComp = objReader["telefono"].ToString();
                        // Reemplazar ';' por <br/> para que se muestre correctamente en el frontend
                        TelComp = TelComp.Replace(";", "<br/>");

                        Company objCompany = new Company(nameComp, dirComp, TelComp);

                        colCompanys.Add(objCompany);
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

            return colCompanys;
        }
    }
}
