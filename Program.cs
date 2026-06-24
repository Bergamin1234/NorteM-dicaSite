using Microsoft.EntityFrameworkCore;
using Nortemedica.Application.Features.Alligator.Interfaces;
using Nortemedica.Application.Features.Products.Commands.SynchronizeProductBySku;
using Nortemedica.Domain.RepositoryInterfaces;
using Nortemedica.Infrastructure.Data;
using Nortemedica.Infrastructure.Data.Repositories;
using Nortemedica.Infrastructure.Integration.Alligator;

var builder = WebApplication.CreateBuilder(args);

// 1. Adicionar serviços ao contêiner de DI.

// Configuração do MediatR para encontrar automaticamente todos os handlers no assembly da Aplicação.
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(SynchronizeProductBySkuCommand).Assembly));

// Configuração do Entity Framework Core e do DbContext.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Registro dos Repositórios e do Unit of Work.
// Usamos AddScoped para que a mesma instância seja usada em toda uma requisição HTTP.
builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
// Adicionar ICategoryRepository aqui quando for implementado.
// builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Registro dos Serviços de Infraestrutura.
builder.Services.AddScoped<IAlligatorSynchronizationService, AlligatorSynchronizationService>();

// Configuração do HttpClient para o AlligatorApiClient.
builder.Services.AddHttpClient<IAlligatorApiClient, AlligatorApiClient>(client =>
{
    // A URL base da API do Alligator viria da configuração (appsettings.json).
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("AlligatorApi:BaseUrl"));
    // Outras configurações como headers de autenticação podem ser adicionadas aqui.
});


builder.Services.AddControllers();

// Adicionar suporte ao Swagger/OpenAPI para documentação da API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 2. Configurar o pipeline de requisições HTTP.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();