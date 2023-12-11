using ParkingAPI.Tracing.Interfaces;

namespace ParkingAPI.Tracing
{
    public class TracingService : ITracingService
    {
        public string TraceId()
        {
            var current = System.Diagnostics.Activity.Current;
            if (current == null)
            {
                return null;
            }

            return current.Id;
        }
    }
}
