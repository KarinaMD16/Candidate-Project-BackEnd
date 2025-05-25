using CandidateAPI.Entities;

public class Oferta
{
    public int Id { get; set; }

    public string Puesto { get; set; }

    public string Descripcion { get; set; }

    public int EmpresaId { get; set; }

    public Empresa Empresa { get; set; }

    public List<OfertaHabilidad> OfertaHabilidades { get; set; }

    public ICollection<Candidato> CandidatosPostulados { get; set; }

}