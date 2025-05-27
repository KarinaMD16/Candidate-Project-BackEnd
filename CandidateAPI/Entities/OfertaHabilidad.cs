using System.Text.Json.Serialization;

public class OfertaHabilidad
{
    public int OfertaId { get; set; }

    [JsonIgnore]
    public Oferta Oferta { get; set; }

    public int HabilidadId { get; set; }
    public Habilidad Habilidad { get; set; }
}