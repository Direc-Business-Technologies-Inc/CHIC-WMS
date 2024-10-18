using Application.BlazorServer.Registers;
using CurrieTechnologies.Razor.SweetAlert2;
using static Application.Hubs.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();
builder.HelperServices();
builder.Services.AddSweetAlert2();//Added with UserStaticFiles 
var app = builder.Build();


app.MapApplicationHubs();
app.UseResponseCompression();
app.AppServices();


