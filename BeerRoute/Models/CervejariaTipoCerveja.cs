using System.ComponentModel.DataAnnotations;

namespace BeerRoute.Models
{
    public class CervejariaTipoCerveja
    {
        public int Id { get; set; }
        [Required]
        public int CervejariaId { get; set; }
        [Required]
        public int TipoCervejaId { get; set; }
    }
}
