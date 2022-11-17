using System.Text.Json.Serialization;
using Factory;
using Microsoft.AspNetCore.Http.Json;
using WebApi.Filter;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition 
    = JsonIgnoreCondition.WhenWritingNull);

// Eable cors
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddControllers();

//Filters 
builder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionFilter)));

//Dependency Injection
ServiceFactory factory = new ServiceFactory(builder.Services);
factory.AddCustomServices();
factory.AddDbContextService();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
