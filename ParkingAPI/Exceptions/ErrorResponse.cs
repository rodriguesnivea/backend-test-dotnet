using System.Text.Json;

namespace ParkingAPI.Exceptions
{
    public class ErrorResponse 
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public string ApplicationCode {get; set; } 

        public ErrorResponse(string message, int statusCode, string applicationCode)
        {
            Message = message;
            StatusCode = statusCode;
            ApplicationCode = applicationCode;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
