using CSParking.Middlewares;
using CSParking.Models.Database.CsParking.Context;
using CSParking.Services.Algorithms.Card;
using CSParking.Services.Algorithms.Qr;
using CSParking.Services.DataAccess.CsParking;
using CSParking.Services.Initialization;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CSParking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var corsPolicyName = "defautCors";

            var isDevelopment = builder.Environment.IsDevelopment();

            var configurationPath = isDevelopment
                ? "appsettings.Development.json"
                : "appsettings.json";

            builder.Services.AddCors(op =>
            {
                op.AddPolicy(corsPolicyName, policy =>
                {
                    policy.SetIsOriginAllowed(x => new Uri(x).IsLoopback)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(configurationPath);

            var configuration = configurationBuilder.Build();

            var loggingSection = configuration.GetRequiredSection("Logging");
            var logFilePath = loggingSection.GetRequiredSection("LogFilePath").Value;
            var logTemplate = loggingSection.GetRequiredSection("LogTemplate").Value;


            builder.Logging.AddFile(logFilePath, outputTemplate: logTemplate);

            // Add services to the container.
            builder.Services.AddSingleton<IConfiguration>(_ => configuration);

            var connectionString = isDevelopment
                ? configuration.GetConnectionString("MfRADb_Development")
                : configuration.GetConnectionString("MfRADb");

            builder.Services.AddDbContext<CSParkingContext>(op => op.UseSqlServer(connectionString));

            var mainConnectionString = configuration.GetConnectionString("Main");

            builder.Services.AddDbContext<MainContext>(op => op.UseSqlServer(mainConnectionString));

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<UsersDataAccess>();
            builder.Services.AddScoped<CardEventsDataAccess>();
            builder.Services.AddScoped<QrEventsDataAccess>();
            builder.Services.AddScoped<PayTypesDataAccess>();
            builder.Services.AddScoped<EventTypesDataAccess>();
            builder.Services.AddScoped<CardCheckAlgorithm>();
            builder.Services.AddScoped<QrCheckAlgorithm>();

            var app = builder.Build();

            using (var scope  = app.Services.CreateScope())
            {
                var mainContext = scope.ServiceProvider.GetRequiredService<MainContext>();
                DatabaseInitialization.Initialize(mainContext);
            }

            app.UseCors(corsPolicyName);

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<DbConnectionCheckHandler>();

            app.MapControllers();

            app.Run();
        }
    }
}
