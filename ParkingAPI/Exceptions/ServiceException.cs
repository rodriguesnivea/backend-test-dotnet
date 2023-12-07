using System;

namespace ParkingAPI.Exceptions
{
    public class ServiceException : Exception
    {
        public ApplicationError ApplicationError { get; }
        public ServiceException(ApplicationError application) : base()
        {
            ApplicationError = application;
        }
    }
}
