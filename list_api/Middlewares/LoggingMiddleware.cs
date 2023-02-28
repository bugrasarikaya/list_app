namespace list_api.Middlewares {
	public class LoggingMiddleware {
		private readonly RequestDelegate next;
		private readonly ILogger logger;
		public LoggingMiddleware(ILoggerFactory logger_factory, RequestDelegate next) { // Constructing.
			logger = logger_factory.CreateLogger<LoggingMiddleware>();
			this.next = next;
		}
		public async Task Invoke(HttpContext context) { // Logging after accessing to action.
			logger.LogInformation((context.Request.RouteValues["controller"]!.ToString() + "controller and " + context.Request.RouteValues["action"]!.ToString() + " action was accessed."));
			await next(context);
		}
	}
}