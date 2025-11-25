using LockAi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64;
    });

builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoAzure")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSomee", policy =>
        policy.WithOrigins("http://lockai.somee.com")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowSomee");
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.Run();
