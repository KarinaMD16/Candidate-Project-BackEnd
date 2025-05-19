public class Habilidad
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public ICollection<OfertaHabilidad> OfertaHabilidades { get; set; }
}