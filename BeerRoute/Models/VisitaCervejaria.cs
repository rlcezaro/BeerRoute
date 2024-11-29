using System.ComponentModel.DataAnnotations;

namespace BeerRoute.Models
{
    public class VisitaCervejaria
    {
        public int Id { get; set; }
        [Required]
        public int VisitaId { get; set; }
        public Visita Visita { get; set; }
        [Required]
        public int CervejariaId { get; set; }
        public Cervejaria Cervejaria { get; set; }
    }
}
