using Domain.Common;

namespace Domain.Entities
{
    public class Matter : BaseEntity
    {
        public string MatterTitle { get; set; }
        public int Amount { get; set; }
        public string MatterCode { get; set; }
        public string ClientCode { get; set; }
        public Client Client { get; set; }
    }
}
