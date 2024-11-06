using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeerRoute.Models
{
    public class TipoCerveja
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Estilo { get; set; }

        [StringLength(50)]
        public string Pais { get; set; }

        [StringLength(100)]
        public string Fabricante { get; set; }

        [Range(0, 100)]
        public int IBU { get; set; }

        [Range(0, 100)]
        public double ABV { get; set; }

        [StringLength(500)]
        public string Descricao { get; set; }

        [Url]
        public string ImagemUrl { get; set; }

        public ICollection<CervejariaTipoCerveja> CervejariaTiposCervejas { get; set; }
    }
}