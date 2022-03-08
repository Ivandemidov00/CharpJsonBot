using CSharpJson.Application;
using CSharpJson.Application.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

const string configurationFileName = "appsettings.json";

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile(configurationFileName,false,true).AddUserSecrets<Program>();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
app.UseRouting();
app.UseEndpoints(_ =>
{
    _.MapPost("/update",
        async ([FromBody] object update) =>
            await app.Services.GetRequiredService<ICoreService>().ExecuteAsync(update));
    _.MapGet("/start", async () => await app.Services.GetRequiredService<ICoreService>().SetWebHook());
});

app.UseHttpsRedirection();
await app.RunAsync();
