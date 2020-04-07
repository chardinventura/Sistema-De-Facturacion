namespace SistemaFacturacion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Compra
    {
        public int Id { get; set; }

        public int? producto_id { get; set; }

        public int? cliente_id { get; set; }

        public int Cantidad { get; set; }

        public DateTime? Fecha { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
