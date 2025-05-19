namespace CandidateAPI.Entities
{
    public class Candidato
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Password { get; set; }
        public string[] Habilidades { get; set; }
        public string Role { get; set; }
    }

    public class RegisterCandidato
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Password { get; set; }

    }

    public class DTOCandidato
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string[] Habilidades { get; set; }
    }
}
