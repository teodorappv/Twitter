using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Twitter.API.ActionFilters;
using Twitter.API.Execption;
using Twitter.API.Services;
using Twitter.Core.Entities;
using Twitter.Core.Interfaces;
using Twitter.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddDbContext<TwitterAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TwitterAPIContext") ?? throw new InvalidOperationException("Connection string 'TwitterAPIContext' not found.")));

builder.Services.AddScoped<ICategoriesService,CategoriesService>();
builder.Services.AddScoped<ValidateEntityExistsAttribute<Category>>();
builder.Services.AddScoped<ValidationFilterAttribute>();

builder.Services.ConfigureLoggerService();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
