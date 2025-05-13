using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SoapCore;
using Transportadora.Interfaces;
using Transportadora.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o serviço de cache de memória
builder.Services.AddMemoryCache();

// Registra o serviço do transportador
builder.Services.AddSingleton<ITransportadorService, TransportadorService>();

var app = builder.Build();

// Configura o endpoint SOAP
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<ITransportadorService>("/TransportadorService.svc", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
});

app.Run();