
namespace CityInfo.API.Middelware
{
    public class TimingMiddleWare
    {
        private readonly ILogger<TimingMiddleWare> _logger;
        private readonly RequestDelegate _next;
        public TimingMiddleWare(ILogger<TimingMiddleWare> logger, RequestDelegate next)
        {
              _logger = logger ?? throw new ArgumentNullException(nameof(logger));
             _next = next ?? throw new ArgumentNullException(nameof(next));
        }
        public async Task Invoke(HttpContext context) {
            var start = DateTime.UtcNow;
            await _next.Invoke(context);
            _logger.LogInformation($"Reqest {context.Request.Path} :{(DateTime.UtcNow - start).TotalMilliseconds} ms");
        }
    }
}
