using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AccesoDatos.Company.Fact.Proveedor;

namespace Negocio.Company.FactProveedor
{
    public class N_FactProveedor
    {
        AD_FactProveedor ad_FactProveedor = new AD_FactProveedor();
        public DataTable ListaFacturasProveedorPendiente()
        {
            try
            {
                return ad_FactProveedor.ListaFacturasProveedorPendientes();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable ListaFacturasProveedorCancelada()
        {
            try
            {
                return ad_FactProveedor.ListaFacturasProveedorCancelada();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string obtenerURLFactura(int idFactura)
        {
            try
            {
                return ad_FactProveedor.obtenerURLFactura(idFactura);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertarFacturaProveedor(int IdProveedor, DateTime FechaCompra, DateTime FechaVencimiento, decimal Monto, string NumFactura, string pev, string bodega, string url, string Proyecto)
        {
            try
            {
                return ad_FactProveedor.InsertarFacturaProveedor(IdProveedor, FechaCompra, FechaVencimiento, Monto, NumFactura, pev, bodega, url, Proyecto);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ActualizarFacturaProveedor(int IdFactura, int IdProveedor, DateTime FechaCompra, DateTime FechaVencimiento, decimal Monto, string NumFactura, string pev, string bodega, string url, string Proyecto)
        {
            try
            {
                return ad_FactProveedor.ActualizarFacturaProveedor(IdFactura, IdProveedor, FechaCompra, FechaVencimiento, Monto, NumFactura, pev, bodega, url, Proyecto);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ObtenerRutaImagen(int idFactura) {
            return ad_FactProveedor.ObtenerRutaImagen(idFactura);
        }
        public bool ActualizaSaldo(int IdFactura, string Monto) 
        {
            try
            {
                return ad_FactProveedor.ActualizarSaldo(IdFactura, Monto);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool EliminarFacturaProveedor(int IdFactura)
        {
            try
            {
                return ad_FactProveedor.EliminarFactura(IdFactura);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
