using FavoriteMoviesAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Adiciona suporte a controllers
builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração da autenticação JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Configuração de logs estruturados
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Configuração de health checks
builder.Services.AddHealthChecks();

// Configuração manual das portas HTTP e HTTPS (opcional)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5135); // Porta HTTP
    options.ListenAnyIP(7135, listenOptions => listenOptions.UseHttps()); // Porta HTTPS
});

var app = builder.Build();

// Middleware de tratamento de exceções
app.UseExceptionHandler("/error");

// Middleware de Swagger apenas em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware de autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

// Mapeamento dos controllers
app.MapControllers();

// Health check endpoint
app.MapHealthChecks("/health");

// Endpoint simples para verificar se a API está rodando
app.MapGet("/", () => "API está rodando!");

app.Run();




