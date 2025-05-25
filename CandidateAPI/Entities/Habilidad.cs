using CandidateAPI.Entities;
using System.Text.Json.Serialization;

public class Habilidad
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public ICollection<Candidato> Candidatos { get; set; }
    [JsonIgnore]
    public ICollection<Oferta> Ofertas { get; set; }
}