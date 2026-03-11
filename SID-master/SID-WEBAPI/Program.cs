using SID_WEBAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IPortalSettingsService, PortalSettingsService>();

var app = builder.Build();

// Configura o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware para arquivos estáticos e SPA
app.UseDefaultFiles(); // Permite servir index.html na raiz
app.UseStaticFiles();

// app.UseHttpsRedirection(); // Desabilitado para simplificar ambiente local http://localhost:5301

// Mapeia os controllers da API. As rotas serão prefixadas com /api/[controller]
// por convenção nos próprios arquivos de controller.
app.MapControllers();

// Faz o fallback para o index.html do SPA para qualquer rota que não seja
// uma rota de API ou um arquivo estático. Isso é crucial para que o roteamento
// do React (client-side) funcione corretamente.
app.MapFallbackToFile("index.html");

app.Run();
