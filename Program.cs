using web_api.Models;
using web_api.Services;
using web_api.Interfaces;
using web_api.middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// רישום המימוש כ-Singleton, כדי שהנתונים בזיכרון יישמרו בין בקשות
builder.Services.AddSingleton<IBookLibrary, BookLibrary>();

var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseMiddleware<LibraryCloseMiddleware>();

// Configure the HTTP request pipeline
// 
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Swagger UI — מסך ויזואלי לבדיקת ה-API
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "web-api v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
