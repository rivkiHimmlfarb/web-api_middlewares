using Microsoft.Extensions.Logging;

namespace web_api.middlewares;

public class ErrorHandlingMiddleware
{
     private readonly RequestDelegate next;
     private readonly 	ILogger<ErrorHandlingMiddleware> logger;
     public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger){
        this.next = next;
        this.logger = logger;
     }



     public async Task Invoke(HttpContext context){
        try{
                await next.Invoke(context);
           }     
        catch(Exception ex){
logger.LogError(ex, "Unhandled exception for {Method} {Path}",
    context.Request.Method, context.Request.Path);
    context.Response.StatusCode = 500;
            var response = new
            {
                error ="internal server error",
                status = 500

            };
               await context.Response.WriteAsJsonAsync(response);
        }      


     }


}
