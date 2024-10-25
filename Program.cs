using Microsoft.OpenApi.Models;
using RickAndMorty;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRickAndMortyGraphQLClient, RickAndMortyGraphQLClient>();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.UseMemberCasing());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Rick and Morty Open API",
        Description = "An ASP.NET Core Web API Wrapper for Rick and Morty Graphql API",
    });
});


var app = builder.Build();
app.MapControllers();
app.Logger.Log(LogLevel.Trace, "H");
app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});
if (builder.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
app.UseHttpsRedirection();

app.Run();

