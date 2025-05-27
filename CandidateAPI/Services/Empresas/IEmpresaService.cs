using CandidateAPI.DTOs;

namespace CandidateAPI.Services.Empresas
{
    public interface IEmpresaService
    {
        Task<Empresa> crearEmpresa(CrearEmpresaDto dto);
    }
}
