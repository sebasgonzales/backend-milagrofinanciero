// IHomeService
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DTO.response;

namespace Services
{
    public interface ILoginService
    {
        Task<ClienteDtoOut?> AuthenticateCliente(string username, string password);
    }
}
