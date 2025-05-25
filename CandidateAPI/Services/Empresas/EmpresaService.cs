using CandidateAPI.DTOs;
using CandidateAPI.JWTDataBase;

namespace CandidateAPI.Services.Empresas
{
    public class EmpresaService: IEmpresaService
    {
        private readonly JWTDbContext _jWTDbContext;

        public EmpresaService(JWTDbContext jWTDbContext)
        {
            _jWTDbContext = jWTDbContext;
        }

        public async Task<Empresa> crearEmpresa(CrearEmpresaDto dto)
        {
            var empresa = new Empresa
            {
                Nombre = dto.Nombre,
                SitioWeb = dto.SitioWeb,
                Correo = dto.Correo,
            };

            _jWTDbContext.Empresas.Add(empresa);
            await _jWTDbContext.SaveChangesAsync();

            return empresa;
        }
    }
}
