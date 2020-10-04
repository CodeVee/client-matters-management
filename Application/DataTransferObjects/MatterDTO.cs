using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransferObjects
{
    public abstract class MatterDTO
    {
        public string Title { get; set; }
        public int Amount { get; set; }
    }
}
