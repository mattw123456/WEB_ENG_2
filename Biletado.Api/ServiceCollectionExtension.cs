

using Biletado.Api.Buildings;
using Biletado.Api.Room;
using Biletado.Api.Status;
using Biletado.Api.Storey;

namespace Biletado.Api
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IStoreyService, StoreyService>();
            services.AddScoped<IRoomService, RoomService>();
        }
    }
}
