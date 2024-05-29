using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using System.Threading.Tasks;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddSingleton<CosmosClient>(sp =>
        {
            return new CosmosClient(Configuration["CosmosDb:Account"], Configuration["CosmosDb:Key"]);
        });

        // Register services for each container
        services.AddSingleton<ICosmosDbService<Book>>(sp =>
            InitializeCosmosClientInstanceAsync<Book>(
                Configuration["CosmosDb:DatabaseName"],
                Configuration["CosmosDb:ContainerNames:Books"]
            ).GetAwaiter().GetResult());
        services.AddSingleton<ICosmosDbService<Member>>(sp =>
            InitializeCosmosClientInstanceAsync<Member>(
                Configuration["CosmosDb:DatabaseName"],
                Configuration["CosmosDb:ContainerNames:Members"]
            ).GetAwaiter().GetResult());
        services.AddSingleton<ICosmosDbService<Issue>>(sp =>
            InitializeCosmosClientInstanceAsync<Issue>(
                Configuration["CosmosDb:DatabaseName"],
                Configuration["CosmosDb:ContainerNames:Issues"]
            ).GetAwaiter().GetResult());
    }

    private async Task<ICosmosDbService<T>> InitializeCosmosClientInstanceAsync<T>(string databaseName, string containerName)
    {
        CosmosClient client = new CosmosClient(Configuration["CosmosDb:Account"], Configuration["CosmosDb:Key"]);
        CosmosDbService<T> cosmosDbService = new CosmosDbService<T>(client, databaseName, containerName);
        DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
        await database.Database.CreateContainerIfNotExistsAsync(containerName, "/UId");

        return cosmosDbService;
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
