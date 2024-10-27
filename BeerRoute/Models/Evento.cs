namespace BeerRoute.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public int CervejariaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEvento { get; set; }

        public Cervejaria Cervejaria { get; set; }
    }
}

