using System.ComponentModel.DataAnnotations;

namespace BeerRoute.Models
{
    public class Cervejaria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public string Endereco { get; set; }

        [Range(-90, 90, ErrorMessage = "A latitude deve estar entre -90 e 90.")]
        public double Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "A longitude deve estar entre -180 e 180.")]
        public double Longitude { get; set; }
        [Range(0, 1000)]
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        [Phone]
        public string Telefone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Url]
        public string Site { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string ImagemUrl { get; set; }

        public ICollection<CervejariaTipoCerveja> CervejariaTiposCervejas { get; set; }
        public ICollection<Visita> Visitas { get; set; }
        public ICollection<Evento> Eventos { get; set; }
    }
}