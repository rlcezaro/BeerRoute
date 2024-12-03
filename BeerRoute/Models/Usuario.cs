using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeerRoute.Models
{

    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Senha { get; set; }

        [Range(0, int.MaxValue)]
        public int Creditos { get; set; }

        public ICollection<Visita> Visitas { get; set; } = new List<Visita>();
        public ICollection<CompraCredito> ComprasCreditos { get; set; } = new List<CompraCredito>();
    }
}