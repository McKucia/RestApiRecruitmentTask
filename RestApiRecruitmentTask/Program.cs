using Microsoft.OpenApi.Models;
using RestApiRecruitmentTask.Core.Services;
using Microsoft.EntityFrameworkCore;
using RestApiRecruitmentTask.Core.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RestApiRecruitmentTaskDbContext>(options =>
{
    options.UseInMemoryDatabase("SklepOpon");
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen((c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "sklepopon API",
        Version = "v1",
        Description = "API for managing tires and producers."
    });
    var xmlFile = Path.Combine(AppContext.BaseDirectory, "RestApiRecruitmentTask.Api.xml");
    c.IncludeXmlComments(xmlFile);
}));

builder.Services.AddScoped<IProducerService, ProducerService>(); // to save data in memory
builder.Services.AddScoped<ITireService, TireService>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
