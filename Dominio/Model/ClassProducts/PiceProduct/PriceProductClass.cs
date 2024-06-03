namespace Dominio.PriceProduct
{
    public class PriceProductClass
    {
        public int IdPriceProduct { get; set; }
        public int IdProduct {  get; set; }
        public string Nombre { get; set; }
        public string Supplier { get; set; }
        public string Color { get; set; }
        public decimal BasePrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Cost { get; set; }
        public decimal SalePrice1 { get; set; }
        public decimal SalePrice2 { get; set; }
        public decimal Tamaño { get; set; }
    }
}
