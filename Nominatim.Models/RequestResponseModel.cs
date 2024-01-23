using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nominatim.Models
{
    public class RequestResponseModel
    {
        public Status Status { get; set; }
        public string? ErrorMessage { get; set; }

        public List<ApiResponseModel> ApiResponse { get; set; }
    }

    public enum Status
    {
        Error,
        Success
    }
}
