namespace BeerRoute.Models
{
    public class Visita
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int CervejariaId { get; set; }
        public DateTime DataVisita { get; set; }
        public int CreditosUtilizados { get; set; }
        public int Avaliacao { get; set; }
        public string Comentario { get; set; }

        public Usuario Usuario { get; set; }
        public Cervejaria Cervejaria { get; set; }
    }
}
