using System.ComponentModel.DataAnnotations;

namespace BeerRoute.Models
{
    public class CompraCredito
    {
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataCompra { get; set; }
    }
}
