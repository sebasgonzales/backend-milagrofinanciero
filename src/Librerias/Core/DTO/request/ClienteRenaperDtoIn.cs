using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.request
{
    public class ClienteRenaperDtoIn
    {
        public required string clientId { get; set; }
        public required string clientSecret { get; set; }
        public required string authorizationCode { get; set; }
    }
}
