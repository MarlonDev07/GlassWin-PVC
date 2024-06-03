using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Model;
using Dominio.Model.Proveedor;

namespace AccesoDatos.RegProveedor
{
    public class AD_RegProveedor
    {
        DataBase.ClsConnection Cnn = new DataBase.ClsConnection();

        public DataTable ListaProveedores() 
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlDataReader Read;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "Select * from RegProveedor WHERE IdEmpresa = @Id";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", CompanyCache.IdCompany);
                Read = cmd.ExecuteReader();
                dataTable.Load(Read);
                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertarProveedor(cls_Proveedor nuevoProveedor)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = Cnn.OpenConecction();
                // Crear la sentencia INSERT INTO
                command.CommandText = @"INSERT INTO RegProveedor (IdEmpresa, CedulaJuridica, Nombre, Direccion, Correo, Telefono, Apc, DiasCredito, LimiteCredito) 
                                     VALUES (@IdEmpresa, @CedulaJuridica, @Nombre, @Direccion, @Correo, @Telefono, @Apc, @DiasCredito, @LimiteCredito)";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@IdEmpresa", nuevoProveedor.IdEmpresa);
                command.Parameters.AddWithValue("@CedulaJuridica", nuevoProveedor.CedulaJuridica);
                command.Parameters.AddWithValue("@Nombre", nuevoProveedor.Nombre);
                command.Parameters.AddWithValue("@Direccion", nuevoProveedor.Direccion);
                command.Parameters.AddWithValue("@Correo", nuevoProveedor.Correo);
                command.Parameters.AddWithValue("@Telefono", nuevoProveedor.Telefono);
                command.Parameters.AddWithValue("@Apc", nuevoProveedor.Apc);
                command.Parameters.AddWithValue("@DiasCredito", nuevoProveedor.DiasCredito);
                command.Parameters.AddWithValue("@LimiteCredito", nuevoProveedor.LimiteCredito);
                command.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ActualizarProveedor(cls_Proveedor proveedorActualizado)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = Cnn.OpenConecction();

                // Crear la sentencia UPDATE
                command.CommandText = @"UPDATE RegProveedor 
                                SET CedulaJuridica = @CedulaJuridica, 
                                    Nombre = @Nombre, 
                                    Direccion = @Direccion, 
                                    Correo = @Correo, 
                                    Telefono = @Telefono, 
                                    Apc = @Apc, 
                                    DiasCredito = @DiasCredito, 
                                    LimiteCredito = @LimiteCredito
                                WHERE IdProvedor = @IdProveedor";

                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@IdProveedor", proveedorActualizado.IdProveedor);
                command.Parameters.AddWithValue("@CedulaJuridica", proveedorActualizado.CedulaJuridica);
                command.Parameters.AddWithValue("@Nombre", proveedorActualizado.Nombre);
                command.Parameters.AddWithValue("@Direccion", proveedorActualizado.Direccion);
                command.Parameters.AddWithValue("@Correo", proveedorActualizado.Correo);
                command.Parameters.AddWithValue("@Telefono", proveedorActualizado.Telefono);
                command.Parameters.AddWithValue("@Apc", proveedorActualizado.Apc);
                command.Parameters.AddWithValue("@DiasCredito", proveedorActualizado.DiasCredito);
                command.Parameters.AddWithValue("@LimiteCredito", proveedorActualizado.LimiteCredito);
                command.Parameters.AddWithValue("@IdEmpresa", proveedorActualizado.IdEmpresa);

                command.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool EliminarProveedor(int IdProveedor)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Cnn.GetConnectionString()))
                {
                    int idProveedor = IdProveedor;
                    connection.Open();

                    // Inicia una transacción
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Crea y ejecuta las sentencias DELETE dentro de la transacción
                        SqlCommand deleteRegPagosCommand = new SqlCommand("DELETE FROM RegPagos WHERE IdCuenta IN (SELECT IdCuenta FROM CuentaxPagar WHERE IdProvedor = @IdProveedor)", connection, transaction);
                        deleteRegPagosCommand.Parameters.AddWithValue("@IdProveedor", idProveedor);
                        deleteRegPagosCommand.ExecuteNonQuery();

                        SqlCommand deleteCuentaxPagarCommand = new SqlCommand("DELETE FROM CuentaxPagar WHERE IdProvedor = @IdProveedor", connection, transaction);
                        deleteCuentaxPagarCommand.Parameters.AddWithValue("@IdProveedor", idProveedor);
                        deleteCuentaxPagarCommand.ExecuteNonQuery();

                        SqlCommand deleteFacturasProveedorCommand = new SqlCommand("DELETE FROM FacturasProveedor WHERE IdProvedor = @IdProveedor", connection, transaction);
                        deleteFacturasProveedorCommand.Parameters.AddWithValue("@IdProveedor", idProveedor);
                        deleteFacturasProveedorCommand.ExecuteNonQuery();

                        SqlCommand deleteRegProveedorCommand = new SqlCommand("DELETE FROM RegProveedor WHERE IdProvedor = @IdProveedor", connection, transaction);
                        deleteRegProveedorCommand.Parameters.AddWithValue("@IdProveedor", idProveedor);
                        deleteRegProveedorCommand.ExecuteNonQuery();

                        // Si todas las operaciones fueron exitosas, confirma la transacción
                        transaction.Commit();

                        Console.WriteLine("El proveedor y sus registros relacionados han sido eliminados correctamente.");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Si ocurre un error, revierte la transacción
                        Console.WriteLine("Ocurrió un error al eliminar el proveedor y sus registros relacionados:");
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la conexión:");
                Console.WriteLine(ex.Message);
                return false;
            }
        }    
        public DataTable BuscarProveedor(int IdProveedor)
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlDataReader Read;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "Select * from RegProveedor WHERE IdProvedor = @IdProveedor";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                Read = cmd.ExecuteReader();
                dataTable.Load(Read);
                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
