namespace SistemaFacturacion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stock")]
    public partial class Stock
    {
        public int Id { get; set; }

        public int? producto_id { get; set; }

        public int Cantidad { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
