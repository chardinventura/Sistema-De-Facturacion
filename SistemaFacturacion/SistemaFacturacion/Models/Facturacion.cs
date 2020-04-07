namespace SistemaFacturacion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Facturacion")]
    public partial class Facturacion
    {
        public int Id { get; set; }

        public int? cliente_id { get; set; }

        public decimal? Descuento { get; set; }

        public decimal? Itbis { get; set; }

        public decimal? Monto { get; set; }

        public DateTime? Fecha { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
