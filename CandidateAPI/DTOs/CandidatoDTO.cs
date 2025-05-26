namespace CandidateAPI.DTOs
{
    public class CandidatoDTO
    {
        public int id {  get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }

        public List<HabilidadDto> Habilidades { get; set; }

    }
}