using MoodMonitor;
using MoodMonitor.Data;
using MonitorDataAccess.Extensions;
using MoodModule.Extensions;
using StatsModule.Extensions;
using NotesModule.Extensions;
using MoodModule.Entities;
using MatBlazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddBlazorBootstrap();
builder.Services.AddMatBlazor();

// à supprimer à terme
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<CounterEntity>();

builder.Services.AddControllersWithViews();

builder.Services.AddDataAccessServices();
builder.Services.AddDataAccessLogging(builder.Configuration);
builder.Services.ConfigureDataAccessServices(builder.Configuration);

builder.Services.AddMoodServices();
builder.Services.AddMoodLogging(builder.Configuration);
builder.Services.ConfigureMoodServices(builder.Configuration);

builder.Services.AddStatsServices();
builder.Services.AddStatsLogging(builder.Configuration);
builder.Services.ConfigureStatsServices(builder.Configuration);

builder.Services.AddNotesServices();
builder.Services.AddNotesLogging(builder.Configuration);
builder.Services.ConfigureNotesServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/ErrorDev");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAntiforgery();

app.MapRazorComponents<App>()
   //.AddAdditionalAssemblies(typeof(DataAccess.Extensions.MoodServiceExtensions).Assembly)
   .AddInteractiveServerRenderMode();

app.Run();
