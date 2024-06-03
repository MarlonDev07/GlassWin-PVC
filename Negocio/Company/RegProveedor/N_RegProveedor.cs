using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.RegProveedor;
using Dominio.Model.Proveedor;

namespace Negocio.Company.RegProveedor
{
    public class N_RegProveedor
    {
        AD_RegProveedor AD_RegProveedor = new AD_RegProveedor();

        public DataTable ListaProveedores()
        {
            return AD_RegProveedor.ListaProveedores();
        }
        public bool InsertarProveedor(cls_Proveedor nuevoProveedor)
        {
            return AD_RegProveedor.InsertarProveedor(nuevoProveedor);
        }
        public bool ActualizarProveedor(cls_Proveedor ProveedorActualizado)
        {
            return AD_RegProveedor.ActualizarProveedor(ProveedorActualizado);
        }
        public bool EliminarProveedor(int IdProveedor)
        {
            return AD_RegProveedor.EliminarProveedor(IdProveedor);
        }
        public DataTable BuscarProveedor(int IdProveedor)
        {
            return AD_RegProveedor.BuscarProveedor(IdProveedor);
        }
    }
}
