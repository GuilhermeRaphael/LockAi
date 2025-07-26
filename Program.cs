using System.Text;
using LockAi.Data;
using LockAi.Models;
using LockAi.Models.Enuns;
using LockAi.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoLocal")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.MaxDepth = 64; // Opcional, para aumentar a profundidade máxima
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("ConfiguracaoToken:Chave").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();

    // Verifica se o usuário com Login "Danielal" já existe
    if (!context.Usuarios.Any(u => u.Login == "Danielal"))
    {
        Criptografia.CriarPasswordHash("123456", out byte[] hash, out byte[] salt);

        var usuario = new Usuario
        {
            Nome = "Daniela",
            Cpf = "12345678900",
            Login = "Danielal",
            Email = "Daniela@email.com",
            DtNascimento = new DateTime(2002, 5, 20),
            Telefone = "11999999999",
            TipoUsuarioId = 2,
            PasswordHash = hash,
            PasswordSalt = salt,
            Situacao = SituacaoUsuario.Ativo,
            DtSituacao = DateTime.Now,
            IdUsuarioSituacao = 1,
            RepresentanteLegalId = null,
            Imagens = new List<UsuarioImagem>()
        };

        context.Usuarios.Add(usuario);
        context.SaveChanges();
    }
}   // Insere um usuário padrão no banco ao iniciar a aplicação, caso ele ainda não exista.
    // Garante que o sistema tenha um usuário inicial com senha já criptografada para testes ou acesso inicial.




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
