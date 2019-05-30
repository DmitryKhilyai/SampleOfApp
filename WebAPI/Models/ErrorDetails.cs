using Newtonsoft.Json;

namespace WebAPI.Models
{
    public class ErrorDetails
    {
        public string Error { get; set; }
        public int Status { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
