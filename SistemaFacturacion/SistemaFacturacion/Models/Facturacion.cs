namespace SistemaFacturacion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Facturacion")]
    public partial class Facturacion
    {
        public int Id { get; set; }

        public int? cliente_id { get; set; }

        public decimal? Descuento { get; set; }

        public decimal Monto { get; set; }

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

        public int conteo(List<Facturacion> list)
        {
            return list.Count;
        }
        public decimal sumatoria(List<Facturacion> list)
        {
            return list.Sum(f => f.Monto);
        }

        public decimal promedio(List<Facturacion> list)
        {
            if (list.Count != 0)
                return list.Average(f => f.Monto);
            else
                return 0;
        }
        public decimal valorMinimo(List<Facturacion> list)
        {
            if (list.Count != 0)
                return list.Min(f => f.Monto);
            else
                return 0;
        }
        public decimal valorMaximo(List<Facturacion> list)
        {
            if (list.Count != 0)
                return list.Max(f => f.Monto);
            else
                return 0;
        }
    }
}
