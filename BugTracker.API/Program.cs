using BugTracker.API.Data;
using BugTracker.API.Services;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            if (builder.Environment.IsEnvironment("Docker"))
            {
                builder.WebHost.UseUrls("http://*:80");
            }

            var conn = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Missing DefaultConnection");

            var uiOrigin = builder.Configuration["UIOrigin"] ?? "https://localhost:7268";

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(conn));

            builder.Services.AddScoped<PasswordService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorWasm", policy =>
                {
                    policy.WithOrigins(uiOrigin)
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var passwordService = scope.ServiceProvider.GetRequiredService<PasswordService>();
                const int maxRetries = 10;
                var retryDelay = TimeSpan.FromSeconds(5);
                var initialized = false;

                Thread.Sleep(TimeSpan.FromSeconds(5));
                for (int attempt = 1; attempt <= maxRetries && !initialized; attempt++)
                {
                    try
                    {
                        DbInitializer.Initialize(db, passwordService);
                        initialized = true;
                        Console.WriteLine("Database initialized successfully.");
                    }
                    catch (Exception ex) // catch ALL exceptions
                    {
                        Console.WriteLine($"[Attempt {attempt}] DB init failed: {ex.Message}");
                        if (attempt == maxRetries)
                        {
                            Console.WriteLine("Max retries reached. Failing startup.");
                            throw;
                        }
                        Thread.Sleep(retryDelay);
                    }
                }
            }

            if (app.Environment.IsEnvironment("Development"))
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (!app.Environment.IsEnvironment("Docker"))
            {
                app.UseHttpsRedirection();
            }
            app.UseCors("AllowBlazorWasm");
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}