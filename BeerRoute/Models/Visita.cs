using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BeerRoute.Models
{
    public class Visita
    {
        public int Id { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public Usuario Usuario { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataVisita { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Os créditos utilizados devem ser um valor positivo.")]
        public int CreditosUtilizados { get; set; }
        [Range(1, 5, ErrorMessage = "A avaliação deve estar entre 1 e 5.")]
        public int Avaliacao { get; set; }
        public string Comentario { get; set; }
        public ICollection<VisitaCervejaria> VisitaCervejarias { get; set; }
        public string ModoViagem { get; set; } //Adicionado para selecionar o modo viagem
    }
}
