using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Client : BaseEntity
    {
        public Client()
        {
            Matters = new HashSet<Matter>();
        }
        public string ClientName { get; set; }
        public string ClientCode { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<Matter> Matters { get; private set; }
    }
}
