using Microsoft.EntityFrameworkCore;
using TrabalhoApi.Data;
using TrabalhoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// --- REGISTRO DOS SERVICES ---
builder.Services.AddScoped<EmpresaService>();
builder.Services.AddScoped<FuncionarioService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});

// --- AJUSTE DA STRING DE CONEXÃO ---
// No Docker, o servidor geralmente é o nome do serviço definido no docker-compose (ex: 'db')
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? "server=db;database=trabalhoapi;user=root;password=root";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    )
);

var app = builder.Build();

// --- BLOCO PARA CRIAR O BANCO AUTOMATICAMENTE COM DELAY ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        // Dá 10 segundos para o MySQL (container db) inicializar antes de tentar criar tabelas
        Console.WriteLine("Aguardando o MySQL inicializar...");
        System.Threading.Thread.Sleep(10000);

        context.Database.EnsureCreated();
        Console.WriteLine("Banco de dados e tabelas verificados/criados com sucesso.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao criar o banco de dados.");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.MapControllers();

app.Run();