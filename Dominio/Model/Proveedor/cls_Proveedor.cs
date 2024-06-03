using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model.Proveedor
{
    public class cls_Proveedor
    {
        public Int64 IdEmpresa { get; set; }
        public int IdProveedor { get; set; }
        public string CedulaJuridica { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Apc { get; set; }
        public int DiasCredito { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal SaldoInicioMes { get; set; }
        public decimal Cargos { get; set; }
        public decimal Descargos { get; set; }
        public decimal SaldoActual { get; set; }
        public DateTime FechaUltimoPago { get; set; }
        public string DocumentoUltimoPago { get; set; }
        public DateTime FechaUltimaCompra { get; set; }
        public string DocumentoUltimaCompra { get; set; }
        public DateTime FechaApertura { get; set; }
    }
}
