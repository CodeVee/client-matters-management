using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
