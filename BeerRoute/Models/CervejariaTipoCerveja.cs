using System.ComponentModel.DataAnnotations;
namespace BeerRoute.Models
{
    public class CervejariaTipoCerveja
    {
        [Required]
        public int CervejariaId { get; set; }

        public Cervejaria Cervejaria { get; set; }

        [Required]
        public int TipoCervejaId { get; set; }

        public TipoCerveja TipoCerveja { get; set; }
    }
}
