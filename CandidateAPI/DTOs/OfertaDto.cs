namespace CandidateAPI.DTOs
{
    public class OfertaDto
    {
        public int Id { get; set; }
        public string Puesto { get; set; }
        public string Descripcion { get; set; }
        public int EmpresaId { get; set; }

        public string EmpresaNombre { get; set; }
        public List<HabilidadDto> Habilidades { get; set; }
    }

}
