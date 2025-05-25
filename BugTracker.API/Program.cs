using BugTracker.API.Data;
using BugTracker.API.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Verify connection to database
            try
            {
                using var connection = new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
                connection.Open();
                Console.WriteLine("Database connected successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection failed: {ex.Message}");
                throw; // Optional: rethrow to stop startup if DB is critical
            }

            builder.Services.AddScoped<PasswordService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorWasm", policy =>
                {
                    policy.WithOrigins("https://localhost:7268") // <- Your Blazor WebAssembly origin
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var passwordService = scope.ServiceProvider.GetRequiredService<PasswordService>();
                DbInitializer.Initialize(db, passwordService);
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowBlazorWasm");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}