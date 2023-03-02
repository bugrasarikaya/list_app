using list_api.Exceptions;
namespace list_api.Middlewares {
	public class ExceptionMiddleware {
		private readonly ILogger logger;
		private readonly RequestDelegate next;
		public HttpResponse? response;
		public ExceptionMiddleware(ILoggerFactory logger_factory, RequestDelegate next) { // Constructing.
			logger = logger_factory.CreateLogger<ExceptionMiddleware>();
			this.next = next;
		}
		public async Task Invoke(HttpContext context) { // Handling exception.
			try {
				await next(context);
			} catch (ForbiddenException fe) {
				response = context.Response;
				logger.LogError("A client error occurred.");
				response.StatusCode = StatusCodes.Status403Forbidden;
				await response.WriteAsync(fe.Message);
			} catch (ConflictException ce) {
				response = context.Response;
				logger.LogError("A client error occurred.");
				response.StatusCode = StatusCodes.Status409Conflict;
				await response.WriteAsync(ce.Message);
			} catch (NotFoundException nfe) {
				response = context.Response;
				logger.LogError("A client error occurred.");
				response.StatusCode = StatusCodes.Status404NotFound;
				await response.WriteAsync(nfe.Message);
			} catch (Exception e) {
				response = context.Response;
				logger.LogError("A server error occurred.");
				response.StatusCode = StatusCodes.Status500InternalServerError;
				await response.WriteAsync(e.Message);
			}
		}
	}
}