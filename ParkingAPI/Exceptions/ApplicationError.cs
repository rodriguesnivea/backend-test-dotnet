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
        public static readonly ApplicationError VEHICLE_NOT_FOUND_EXCEPTION = new ApplicationError("PAR-001", "Veiculo nao encontrado", HttpStatusCode.NotFound);
        public static readonly ApplicationError COMPANY_NOT_FOUND_EXCEPTION = new ApplicationError("PAR-002", "Empresa nao encontrada", HttpStatusCode.NotFound);
        public static readonly ApplicationError INTERNAL_SERVER_ERROR = new ApplicationError("PAR-003", "Erro interno na aplicacao", HttpStatusCode.InternalServerError);
    }
}
