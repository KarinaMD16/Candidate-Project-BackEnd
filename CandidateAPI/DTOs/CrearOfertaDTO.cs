namespace CandidateAPI.DTOs;
public class CrearOfertaDTO
{
    public string Puesto { get; set; }
    public string Descripcion { get; set; }
    public int EmpresaId { get; set; }
    public List<int> HabilidadesIds { get; set; }
}