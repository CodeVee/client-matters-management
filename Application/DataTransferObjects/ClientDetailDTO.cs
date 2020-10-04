using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransferObjects
{
    public class ClientDetailDTO : ClientListDTO
    {
        public List<MatterListDTO> Matters { get; set; }
    }
}
