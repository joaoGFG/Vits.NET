using VerdantisBusiness;
using VerdantisData;
using Microsoft.EntityFrameworkCore;
using Serilog;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar o Serilog (Logging Estruturado)
builder.Host.UseSerilog((context, configuration) =>
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day));

// 2. Configurar OpenTelemetry (Tracing e Métricas)
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("VerdantisUI"))
    .WithTracing(tracing =>
    {
        tracing.AddAspNetCoreInstrumentation()
               .AddHttpClientInstrumentation()
               .AddConsoleExporter(); // Exporte para console para verificar localmente
    })
    .WithMetrics(metrics =>
    {
        metrics.AddAspNetCoreInstrumentation()
               .AddHttpClientInstrumentation()
               .AddConsoleExporter();
    });

// 3. Configurar Health Checks
builder.Services.AddHealthChecks()
    .AddOracle(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string not found."),
        name: "OracleDB");

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar DbContext com Oracle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar serviços de negócio e repositórios (Dependency Injection)
builder.Services.AddScoped<IProdutorService, ProdutorService>();
builder.Services.AddScoped<IProdutorRepository, ProdutorRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

// Adicionar middleware do Serilog para logar requisições HTTP
app.UseSerilogRequestLogging();

// Mapear endpoint do Health Check
app.MapHealthChecks("/health", new HealthCheckOptions
{
    // Retorna detalhes completos em JSON (pode ser customizado com pacotes como UIResponseWriter)
    Predicate = _ => true
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

public partial class Program { }