using Follow_Up.API.Extensions;
using Follow_Up.Application.Middleware;
using Follow_Up.Application.Models.Options;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var appSettings = new AppSettings();
builder.Configuration.Bind(nameof(AppSettings), appSettings);
builder.Services.AddSingleton(appSettings);

builder.Services.AddApplicationLayerAPI(builder.Configuration["ConnectionStrings:SqlConnection"]);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizationOptions.Value);

app.UseMiddleware<ExceptionCatcherMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
