using Comunication;
using Comunication.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.AddConfigSettings();

builder.Services.AddServices();

var host = builder.Build();
host.Run();
