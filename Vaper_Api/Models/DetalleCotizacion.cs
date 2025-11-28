using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vaper_Api.Models
{
    public partial class DetalleCotizacion
    {
        [Key]
        public int Id { get; set; }

        public int CotizacionId { get; set; }

        public int ProductoId { get; set; }

        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal PrecioUnitario { get; set; }

        [ForeignKey("CotizacionId")]
        [InverseProperty("DetalleCotizaciones")]
        public virtual Cotizacion Cotizacion { get; set; }

        [ForeignKey("ProductoId")]
        [InverseProperty("DetalleCotizaciones")]
        public virtual Producto Producto { get; set; }
    }
}