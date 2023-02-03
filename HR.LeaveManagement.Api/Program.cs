using HR.LeaveManagement.Application;
using HR.LeaveManagement.Infrastructure;
using HR.LeaveManagement.Persistence;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(config);
builder.Services.ConfigurePersistenceServices(config);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HR.LeaveManagement.Api", Version = "v1" });
});

//add cors policy - how it allows other clients to interact with the api
builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", 
    builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HR.LeaveManagement.Api v1"));
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

});

/*app.MapControllers();*/

app.Run();
