using Application.Hubs;
using Application.Libraries.DataAccess;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    Func<string, string> GetConn = (name) => builder.Configuration.GetConnectionString(name);

    var addonConn = new SqlConnectionStringBuilder(GetConn("CommonDb"));
    var sapConn = new SqlConnectionStringBuilder(GetConn("SAP"));
    var sl = builder.Configuration.GetSection("SapServiceLayer").Get<SapServiceLayerLogin>();

    string connsInfo = $"""
                Sap Connection: {sapConn.DataSource} | {sapConn.InitialCatalog}
                Addon Connection: {addonConn.DataSource} | {addonConn.InitialCatalog}
                Service Layer Connection: {sl.Uri} | {sl.CompanyDB}
    """;

    bool showConnections = builder.Environment.IsDevelopment() || builder.Environment.IsStaging();
    if (!showConnections) connsInfo = "";

    opt.CustomSchemaIds(type => type.ToString());
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "TIMS Web Api",
        Description = connsInfo,
        //TermsOfService = new Uri("https://example.com/terms"),
        /*Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }*/
    });
});

builder.Services.AddSignalR(e => {
	e.MaximumReceiveMessageSize = 102400000;
});

builder.Services.AddAutoMapper(
    typeof(Application.Models.Registers.AutoMapperRegisters),
    typeof(Application.Libraries.Mappers.ServiceLayerEnumMapper),
    typeof(Application.Libraries.Mappers.DocumentMapper),
    typeof(Application.Libraries.Registers.AutoMapperRegisters)
);

DataManager.Models.DependencyInjection.AddDataModels(builder.Services);
DataManager.Libraries.DependencyInjection.AddDataLibraries(builder.Services);
DataManager.Services.DependencyInjection.AddDataServices(builder.Services);

Application.Services.DependencyInjection.AddServices(builder.Services);
Application.Libraries.DependencyInjection.AddLibraries(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapApplicationHubs();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
