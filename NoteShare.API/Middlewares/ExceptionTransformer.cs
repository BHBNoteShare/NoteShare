using AutoWrapper.Wrappers;

namespace NoteShare.API.Middlewares
{
    public class ExceptionTransformer
    {
        private readonly RequestDelegate _next;

        public ExceptionTransformer(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (ex is ApiException) throw;

                throw new ApiException(ex);
            }
        }
    }

    public static class ExceptionTransformerExtensions
    {
        public static IApplicationBuilder UseExceptionTransformer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionTransformer>();
        }
    }
}
