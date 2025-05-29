using Biletado.Api.Api.Endpunkte;
using Biletado.Api.Api.Extensions;
using Biletado.Api.Room;
using Biletado.Api.Status;
using Biletado.Api.Storey;

namespace Biletado.Api.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddEnvironmentVariables();

            var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    name: myAllowSpecificOrigins,
                    policy =>
                    {
                        policy
                            .AllowAnyOrigin() // Allow all origins
                            .AllowAnyHeader() // Allow all headers
                            .AllowAnyMethod(); // Allow all methods
                    }
                );
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplicationServices();
            builder.Services.AddPostgresDbContext(builder.Configuration);


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(myAllowSpecificOrigins);
            app.UseHttpsRedirection();

            app.AddStatusEndpunkte();
            app.AddBuildingEndpunkte();
            app.AddStoreyEndpunkte();
            app.AddRoomEndpunkte();

            app.Run();
        }
    }
}
