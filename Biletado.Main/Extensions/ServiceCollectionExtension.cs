using Biletado.Application.Services;
using Biletado.Application.Wrapper;
using Biletado.Domain.Building;
using Biletado.Domain.Room;
using Biletado.Domain.Status;
using Biletado.Domain.Storey;

namespace Biletado.Main.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IStoreyService, StoreyService>();
            services.AddScoped<IRoomService, RoomService>();
            services.Decorate<IRoomService, RoomServiceWrapper>();
        }
    }
}
