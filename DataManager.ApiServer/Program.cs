using DataManager.ApiServer.Registers;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

app.AppServices();
