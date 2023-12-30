using System.Globalization;
using BiteRight.Web.Middleware;
using BiteRight.Web.Registration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateSlimBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

WebRegistrations.AddBiteRightWeb(builder.Services, builder.Configuration);
OptionsRegistrations.AddBiteRightOptions(builder.Services, builder.Configuration);
ApplicationRegistrations.AddBiteRightApplication(builder.Services);
InfrastructureRegistrations.AddBiteRightInfrastructure(builder.Services, builder.Configuration, builder.Environment);
DomainRegistrations.AddBiteRightDomain(builder.Services);

var app = builder.Build();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseSerilogRequestLogging(opt =>
{
    opt.MessageTemplate = "HTTP {RequestMethod} {RequestPath} CorrelationId {CorrelationId} responded {StatusCode} in {Elapsed:0.0000} ms";
    opt.GetLevel = (ctx, _, _) => LogEventLevel.Information;
});
app.UseMiddleware<TransactionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();