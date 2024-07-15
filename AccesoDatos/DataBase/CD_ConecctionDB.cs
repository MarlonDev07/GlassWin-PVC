﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AccesoDatos.DataBase
{
    public class ClsConnection
    {
        private SqlConnection Conexion;
        private string connectionString;

        public ClsConnection()
        {
            if (CompanyCache.IdCompany == 112540885)
            {
                connectionString = ConfigurationManager.ConnectionStrings["VidriosAltura"].ConnectionString;
                Conexion = new SqlConnection(connectionString);
            } /*else if (CompanyCache.IdCompany == 31028013) {
                connectionString = ConfigurationManager.ConnectionStrings["InnovaGlassDB2"].ConnectionString;
                Conexion = new SqlConnection(connectionString);
            }*/
            else
            {
                if (CompanyCache.IdCompany == 31025820)
                {
                    connectionString = ConfigurationManager.ConnectionStrings["GWAluvi"].ConnectionString;
                    Conexion = new SqlConnection(connectionString);
                }
                else
                {
                    connectionString = ConfigurationManager.ConnectionStrings["GlassWinDB"].ConnectionString;
                    Conexion = new SqlConnection(connectionString);
                }
                
            }
        }

        public SqlConnection OpenConecction()
        {
            try
            {
                if (Conexion.State == ConnectionState.Closed)
                {
                    Conexion.ConnectionString = connectionString;
                    Conexion.Open();
                }
                return Conexion;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SqlConnection CloseConnection()
        {
            try
            {
                if (Conexion.State == ConnectionState.Open)
                    Conexion.Close();
                return Conexion;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetConnectionString()
        {
            return connectionString;
        }
    }
}
