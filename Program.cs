// 1. Adicionar serviços ao contêiner de DI.
using Polly;

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
builder.Services
    .AddHttpClient<IAlligatorApiClient, AlligatorApiClient>(client =>
    {
        // A URL base da API do Alligator viria da configuração (appsettings.json).
        client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("AlligatorApi:BaseUrl"));
        // Outras configurações como headers de autenticação podem ser adicionadas aqui.
    })
    // Adiciona uma política de retry para lidar com falhas transitórias de rede.
    .AddTransientHttpErrorPolicy(policyBuilder => 
        policyBuilder.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), 
            onRetry: (outcome, timespan, retryAttempt, context) =>
            {
                // Log da tentativa de retry
            }))
    // Adiciona uma política de Circuit Breaker para evitar sobrecarregar uma API que está falhando.
    .AddTransientHttpErrorPolicy(policyBuilder => 
        policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30))
    );
