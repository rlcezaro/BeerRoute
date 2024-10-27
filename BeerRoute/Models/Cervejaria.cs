using BeerRoute.Models;
using System.ComponentModel;

public class Cervejaria
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public TipoCerveja TipoCerveja { get; set; } // This is the enum from the other snippet
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public decimal Preco { get; set; }
    public string Descricao { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Site { get; set; }
    public string Facebook { get; set; }
    public string Instagram { get; set; }

    public ICollection<Visita> Visitas { get; set; }
    public ICollection<Evento> Eventos { get; set; }
}
