using System.ComponentModel.DataAnnotations;

namespace BeerRoute.Models

{
    public class Evento
    {
        public int Id { get; set; }

        [Required]
        public int CervejariaId { get; set; }

        [Required]
        public Cervejaria Cervejaria { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        public string Descricao { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "A data do evento deve ser uma data e hora válida.")]
        public DateTime DataEvento { get; set; }
    }
}
