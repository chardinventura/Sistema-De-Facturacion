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

        public decimal? Monto { get; set; }

        public DateTime? Fecha { get; set; }

        public virtual Cliente Cliente { get; set; }

        public decimal descuento(decimal cantidad, string categoriaCliente)
        {
            if (categoriaCliente.ToLower().Equals("premium"))
                cantidad -= (cantidad * 0.25m);

            return cantidad;
        }

        public decimal itbis(decimal cantidad)
        {
            cantidad += (cantidad * 0.18m);

            return cantidad;
        }
    }
}
