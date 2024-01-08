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
        // AJUSTAR NOMES
        public static readonly ApplicationError VEHICLE_NOT_FOUND_EXCEPTION = new ApplicationError("VEHICLE_NOT_FOUND", "Veiculo nao encontrado", HttpStatusCode.NotFound);
        public static readonly ApplicationError COMPANY_NOT_FOUND_EXCEPTION = new ApplicationError("COMPANY_NOT_FOUND", "Empresa nao encontrada", HttpStatusCode.NotFound);
        public static readonly ApplicationError PARKING_NOT_FOUND_EXCEPTION = new ApplicationError("PARKING_NOT_FOUND", "Registro de estacionamento nao encontrado", HttpStatusCode.NotFound);
        public static readonly ApplicationError INTERNAL_SERVER_ERROR = new ApplicationError("INTERNAL_SERVER_ERROR", "Erro interno na aplicacao", HttpStatusCode.InternalServerError);
        public static readonly ApplicationError COMPANY_NO_CONTENT_EXCEPTION = new ApplicationError("COMPANY_NO_CONTENT", "Nenhuma empresa disponivel", HttpStatusCode.NoContent);
        public static readonly ApplicationError VEHICLE_NO_CONTENT_EXCEPTION = new ApplicationError("VEHICLE_NO_CONTENT", "Nenhum veiculo diponivel", HttpStatusCode.NoContent);
        public static readonly ApplicationError VEHICLE_ALREADY_PARKED_EXCEPTION = new ApplicationError("VEHICLE_ALREADY_PARKED", "Veiculo ja esta estacionado", HttpStatusCode.BadRequest);
        public static readonly ApplicationError FILLED_CAR_SPOTS_EXCEPTION = new ApplicationError("FILLED_CAR_SPOTS", "Nenhuma vaga para carro disponivel", HttpStatusCode.BadRequest);
        public static readonly ApplicationError FILLED_MOTORCYCLE_SPOTS_EXCEPTION = new ApplicationError("FILLED_MOTORCYCLE_SPOTS", "Nenhuma vaga para moto disponivel", HttpStatusCode.BadRequest);

    }
}
