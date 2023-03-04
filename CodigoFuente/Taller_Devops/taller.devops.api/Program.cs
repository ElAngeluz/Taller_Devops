using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using taller.devops.api.Extensions;
using taller.devops.application.ioc;
using taller.devops.infraestructura.extensions;
using taller.devops.infraestructura.ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.ExampleFilters();

    var version = builder.Configuration["OpenApi:info:version"];
    var title = builder.Configuration["OpenApi:info:title"];
    var description = builder.Configuration["OpenApi:info:description"];
    var termsOfService = new Uri(builder.Configuration["OpenApi:info:termsOfService"]);
    var contact = new OpenApiContact
    {
        Name = builder.Configuration["OpenApi:info:contact:name"],
        Url = new Uri(builder.Configuration["OpenApi:info:contact:url"]),
        Email = builder.Configuration["OpenApi:info:contact:email"]
    };
    var license = new OpenApiLicense
    {
        Name = builder.Configuration["OpenApi:info:License:name"],
        Url = new Uri(builder.Configuration["OpenApi:info:License:url"])
    };
    options.SwaggerDoc(builder.Configuration["OpenApi:info:version"], new OpenApiInfo
    {
        Version = builder.Configuration["OpenApi:info:version"],
        Title = title,
        Description = description,
        TermsOfService = termsOfService,
        Contact = contact,
        License = license
    });
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Put **_ONLY_** your JWT Bearer **_token_** on textbox below! \r\n\r\n\r\n Example: \"Value: **12345abcdef**\"",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
    xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

});
builder.Services.AddSwaggerExamples();
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
builder.Services.AddHealthChecks();
builder.Services.RegisterDependencies();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();



builder.Services.RegisterDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.ConfigureExceptionHandler();
app.UseAuthorization();


app.MapHealthChecks("/health/readiness", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    },
});

app.MapHealthChecks("/health/liveness", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    },
    Predicate = _ => false
});

app.MapControllers();

app.Run();
