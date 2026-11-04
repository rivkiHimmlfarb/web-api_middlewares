namespace web_api.middlewares;
public class LibraryCloseMiddleware{
    private readonly RequestDelegate next;
    public LibraryCloseMiddleware(RequestDelegate next){
    this.next = next;
    }
    public async Task Invoke(HttpContext context, IConfiguration config){
     if (config.GetValue<bool>("LibraryClosed"))
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable; // 503
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("The library is closed. Please come back later.");
            return; // short-circuit: next is NEVER called
        }

        await next.Invoke(context);
    }
    
    
    }