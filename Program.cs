using LockAi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Serialização de enums como string
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64;
    });

// Swagger/OpenAPI
builder.Services.AddOpenApi();

// Conexão com banco de dados
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoSomee")));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSomee", policy =>
        policy.WithOrigins("http://lockai.somee.com")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting(); // <- necessário para mapear rotas
app.UseCors("AllowSomee"); // <- precisa vir entre Routing e Authorization
app.UseAuthorization(); // <- mesmo que não esteja usando autenticação, é padrão

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers(); // <- mapeia os endpoints

app.Run();
