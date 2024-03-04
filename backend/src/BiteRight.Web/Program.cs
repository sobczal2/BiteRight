// # ==============================================================================
// # Solution: BiteRight
// # File: Program.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using AspNetCoreRateLimit;
using Azure.Identity;
using Azure.Storage.Blobs;
using BiteRight.Web.Middleware;
using BiteRight.Web.Registration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

#endregion

Console.WriteLine("Starting BiteRight.Web");

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        options.Connect(
                builder.Configuration.GetConnectionString("AppConfig"))
            .ConfigureKeyVault(kv => { kv.SetCredential(new DefaultAzureCredential()); });
    });
}

var loggerConfiguration = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration);

if (builder.Environment.IsProduction())
{
    var blobServiceClient = new BlobServiceClient(builder.Configuration.GetConnectionString("BlobStorage"));
    loggerConfiguration = loggerConfiguration.WriteTo.AzureBlobStorage(
        blobServiceClient,
        storageContainerName: "logs",
        restrictedToMinimumLevel: LogEventLevel.Information
    );
}

Log.Logger = loggerConfiguration
    .CreateLogger();

builder.Host.UseSerilog();

WebRegistrations.AddBiteRightWeb(builder.Services, builder.Configuration);
OptionsRegistrations.AddBiteRightOptions(builder.Services, builder.Configuration);
ApplicationRegistrations.AddBiteRightApplication(builder.Services);
InfrastructureRegistrations.AddBiteRightInfrastructure(builder.Services, builder.Configuration, builder.Environment);
DomainRegistrations.AddBiteRightDomain(builder.Services);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseMiddleware<CorrelationIdMiddleware>();

app.UseSerilogRequestLogging(opt =>
{
    opt.MessageTemplate =
        "HTTP {RequestMethod} {RequestPath} CorrelationId {CorrelationId} responded {StatusCode} in {Elapsed:0.0000} ms";
    opt.GetLevel = (
        ctx,
        _,
        _
    ) => LogEventLevel.Information;
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<IdentityIdHeaderMiddleware>();
app.UseClientRateLimiting();

app.MapControllers();

app.Run();