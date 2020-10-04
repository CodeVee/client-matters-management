using System.Collections.Generic;

namespace Application.DataTransferObjects
{
    public class ClientDetailDTO : ClientListDTO
    {
        public List<MatterListDTO> Matters { get; set; }
    }
}
