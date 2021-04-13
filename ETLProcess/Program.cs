using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ETLProcess.Model;
using Microsoft.EntityFrameworkCore;
using ETLProcess.Interface.IRepository;
using ETLProcess.Repository;


namespace ETLProcess
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new ConfigurationBuilder()
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                    optional: true)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    
                    IConfiguration configuration = hostContext.Configuration;
                    services.AddDbContext<ETLDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("default")),ServiceLifetime.Singleton);
                    services.AddSingleton<IDbConnection>(db => new SqlConnection(configuration.GetConnectionString("default")));
                    services.AddTransient<IProductionRepo, ProductionRepository>();
                    services.AddTransient<ILineItemRepo, LineItemRepository>();
                    services.AddHostedService<Worker>();
                    
                });
    }
}
