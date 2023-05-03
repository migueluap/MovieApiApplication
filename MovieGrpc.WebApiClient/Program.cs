using Microsoft.OpenApi.Models;
using MovieGrpc.WebApiClient.GrpcServices;
using ProtoDefinitions;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Api Challenge", 
        Version = "v1" 
    });

    var xmlFile = "MovieGrpc.WebApiClient.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory,xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<MovieGrpcServices>();
builder.Services.AddGrpcClient<MoviesApi.MoviesApiClient>(
    options => options.Address = new Uri(builder.Configuration["RPCSettings:MovieUrl"].ToString()
    ));
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Challenge" ));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
