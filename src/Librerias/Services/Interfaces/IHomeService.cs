﻿// IHomeService
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DTO.response;

namespace Services
{
    public interface IHomeService
    {
        Task<IEnumerable<ClienteXCuentaDtoOut>> GetCuentasByClienteUsername(string username);
    }
}
