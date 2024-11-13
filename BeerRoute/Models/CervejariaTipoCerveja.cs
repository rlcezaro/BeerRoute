using System.ComponentModel.DataAnnotations;

namespace BeerRoute.Models
{
    public class CervejariaTipoCerveja
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Cervejaria")]
        public int CervejariaId { get; set; }
        [Required]
        [Display(Name = "Tipo de Cerveja")]
        public int TipoCervejaId { get; set; }

        public Cervejaria Cervejaria { get; set; }
        public TipoCerveja TipoCerveja { get; set; }
    }
}
