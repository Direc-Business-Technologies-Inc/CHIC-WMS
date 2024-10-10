using Application.BlazorServer.Registers;
using static Application.Hubs.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();
builder.HelperServices();

var app = builder.Build();


app.MapApplicationHubs();
app.UseResponseCompression();

app.AppServices();
