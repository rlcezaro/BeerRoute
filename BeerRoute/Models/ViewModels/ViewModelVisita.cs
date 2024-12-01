using System;
using System.Collections.Generic;

namespace BeerRoute.Models.ViewModels
{
    public class ViewModelVisita
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataVisita { get; set; }
        public int CreditosUtilizados { get; set; }
        public int Avaliacao { get; set; }
        public string Comentario { get; set; }
        public string EstiloCerveja { get; set; } // Lista os estilos de cerveja
        public List<int> CervejariaIds { get; set; }
    }
}
