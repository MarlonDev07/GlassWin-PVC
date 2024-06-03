﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Company.Fact.Proveedor
{
    public class AD_FactProveedor
    {
        DataBase.ClsConnection Cnn = new DataBase.ClsConnection();

        public DataTable ListaFacturasProveedorPendientes() 
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlDataReader Read;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "Select RegProveedor.IdProvedor ,RegProveedor.Nombre, FacturaProveedor.IdFactura, FacturaProveedor.NumFactura, FacturaProveedor.FechaCompra, FacturaProveedor.FechaVencimiento, FacturaProveedor.Monto\r\nFrom FacturaProveedor\r\nINNER JOIN RegProveedor\r\nON FacturaProveedor.IdProveedor = RegProveedor.IdProvedor WHERE FacturaProveedor.IdEmpresa = @Id and FacturaProveedor.Monto != 0";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", CompanyCache.IdCompany);
                Read = cmd.ExecuteReader();
                dataTable.Load(Read);
                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListaFacturasProveedorCancelada()
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlDataReader Read;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "Select RegProveedor.IdProvedor ,RegProveedor.Nombre, FacturaProveedor.IdFactura, FacturaProveedor.NumFactura, FacturaProveedor.FechaCompra, FacturaProveedor.FechaVencimiento, FacturaProveedor.Monto\r\nFrom FacturaProveedor\r\nINNER JOIN RegProveedor\r\nON FacturaProveedor.IdProveedor = RegProveedor.IdProvedor WHERE FacturaProveedor.IdEmpresa = @Id and FacturaProveedor.Monto = 0";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", CompanyCache.IdCompany);
                Read = cmd.ExecuteReader();
                dataTable.Load(Read);
                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool InsertarFacturaProveedor(int IdProveedor, DateTime FechaCompra,DateTime FechaVencimiento, string Monto, string NumFactura)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string query = @"INSERT INTO FacturaProveedor (IdEmpresa, IdProveedor, FechaCompra, FechaVencimiento, Monto, NumFactura)
                         VALUES (@IdEmpresa, @IdProveedor, @FechaCompra, @FechaVencimiento, @Monto, @NumFactura)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdEmpresa", CompanyCache.IdCompany);
                cmd.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                cmd.Parameters.AddWithValue("@FechaCompra", FechaCompra);
                cmd.Parameters.AddWithValue("@FechaVencimiento", FechaVencimiento);
                cmd.Parameters.AddWithValue("@Monto", Monto);
                cmd.Parameters.AddWithValue("@NumFactura", NumFactura);

                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarFacturaProveedor(int IdFactura, int IdProveedor, DateTime FechaCompra, DateTime FechaVencimiento, string Monto, string NumFactura)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string query = @"UPDATE FacturaProveedor SET IdProveedor = @IdProveedor, FechaCompra = @FechaCompra, FechaVencimiento = @FechaVencimiento, Monto = @Monto, NumFactura = @NumFactura
                         WHERE IdFactura = @IdFactura";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdFactura", IdFactura);
                cmd.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                cmd.Parameters.AddWithValue("@FechaCompra", FechaCompra);
                cmd.Parameters.AddWithValue("@FechaVencimiento", FechaVencimiento);
                cmd.Parameters.AddWithValue("@Monto", Convert.ToDecimal(Monto));
                cmd.Parameters.AddWithValue("@NumFactura", NumFactura);

                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ActualizarSaldo(int IdFactura, string Monto) 
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string query = @"UPDATE FacturaProveedor SET Monto = @Monto
                         WHERE IdFactura = @IdFactura";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdFactura", IdFactura);
                cmd.Parameters.AddWithValue("@Monto", Convert.ToDecimal(Monto));

                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarFactura(int idFactura)
        {
            using (SqlConnection connection = new SqlConnection(Cnn.GetConnectionString()))
            {
                connection.Open();

                // Comienza la transacción
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Sentencia DELETE para eliminar de la tabla RegPagos
                    string deleteRegPagosQuery = @"DELETE FROM RegPagos WHERE IdCuenta IN (SELECT IdFactura FROM CuentasxPagar WHERE IdFactura = @IdFactura)";

                    // Sentencia DELETE para eliminar de la tabla CuentasxPagar
                    string deleteCuentasxPagarQuery = @"DELETE FROM CuentasxPagar WHERE IdFactura = @IdFactura";

                    // Sentencia DELETE para eliminar de la tabla FacturaProveedor
                    string deleteFacturaProveedorQuery = @"DELETE FROM FacturaProveedor WHERE IdFactura = @IdFactura";

                    // Ejecutar las consultas DELETE dentro de la transacción
                    using (SqlCommand command = new SqlCommand(deleteRegPagosQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@IdFactura", idFactura);
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand(deleteCuentasxPagarQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@IdFactura", idFactura);
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand(deleteFacturaProveedorQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@IdFactura", idFactura);
                        command.ExecuteNonQuery();
                    }

                    // Commit si todas las operaciones se realizan correctamente
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    // Rollback en caso de error
                    Console.WriteLine("Ocurrió un error: " + ex.Message);
                    transaction.Rollback();
                    return false;
                    throw; // Lanza la excepción para que pueda ser manejada en un nivel superior si es necesario
                }
            }
        }
        }
}
