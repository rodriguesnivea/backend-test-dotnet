using System.Collections.Generic;
using System.Net;

namespace ParkingAPI.Exceptions
{
    public class ApplicationError
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ApplicationError(string code, string message, HttpStatusCode statusCode)
        {
            Code = code;
            Message = message;
            StatusCode = statusCode;
        }

        // erros da aplicação
        public static readonly ApplicationError VEHICLE_NOT_FOUND_EXCEPTION = new ApplicationError("VEHICLE_NOT_FOUND", "Veiculo nao encontrado", HttpStatusCode.NotFound);
        public static readonly ApplicationError COMPANY_NOT_FOUND_EXCEPTION = new ApplicationError("COMPANY_NOT_FOUND", "Empresa nao encontrada", HttpStatusCode.NotFound);
        public static readonly ApplicationError PARKING_NOT_FOUND_EXCEPTION = new ApplicationError("PARKING_NOT_FOUND", "Registro de estacionamento nao encontrado", HttpStatusCode.NotFound);
        public static readonly ApplicationError INTERNAL_SERVER_ERROR = new ApplicationError("INTERNAL_SERVER_ERROR", "Erro interno na aplicacao", HttpStatusCode.InternalServerError);       
    }
}
