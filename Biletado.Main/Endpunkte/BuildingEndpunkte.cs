using Biletado.Application.Services;
using Biletado.Domain.Building;
using Microsoft.AspNetCore.Mvc;

namespace Biletado.Main.Endpunkte
{
    public static class BuildingEndpunkte
    {
        public static void AddBuildingEndpunkte(this IEndpointRouteBuilder app)
        {
            app.MapGet(
                    "/api/v3/assets/buildings",
                    async (
                        [FromServices] IBuildingService service,
                        ILogger<BuildingService> logger,
                        [FromQuery] bool include_deleted = false
                    ) =>
                    {
                        logger.LogInformation("Fetching all buildings (include_deleted={include_deleted})", include_deleted);

                        var buildings = await service.GetAllBuildingsAsync(include_deleted);
                        return new { buildings };
                    }
                )
                .WithName("GetAllBuildings")
                .WithOpenApi()
                .WithTags("Buildings");

            app.MapGet(
                    "/api/v3/assets/buildings/{id}",
                    async ([FromServices] IBuildingService service, Guid id, ILogger<BuildingService> logger) =>
                    {
                        logger.LogInformation("Fetching building with ID {id}", id);

                        var building = await service.GetBuildingByIdAsync(id);
                        return building is not null
                            ? Results.Ok(building)
                            : Results.NotFound(new { Message = "Building not found." });
                    }
                )
                .WithName("GetBuildingById")
                .WithOpenApi()
                .WithTags("Buildings");

            app.MapPost(
                    "/api/v3/assets/buildings",
                    async (
                        [FromServices] IBuildingService service,
                        [FromBody] Building building,
                        ILogger<BuildingService> logger
                    ) =>
                    {
                        logger.LogInformation("User creates a building: {@building}", building);

                        if (building == null)
                            return Results.BadRequest("Building data is required.");

                        if (string.IsNullOrEmpty(building.name))
                            return Results.BadRequest(new { Message = "Name is required." });

                        // Weitere Validierungen können hier hinzugefügt werden...

                        var createdBuilding = await service.CreateBuildingAsync(building);

                        logger.LogInformation("Building created with ID {id} by user", createdBuilding.id);

                        return Results.Created(
                            $"/api/v3/assets/buildings/{createdBuilding.id}",
                            createdBuilding
                        );
                    }
                )
                .WithName("CreateBuilding")
                .WithOpenApi()
                .WithTags("Buildings");

            app.MapPut(
                    "/api/v3/assets/buildings/{id}",
                    async (
                        [FromServices] IBuildingService service,
                        Guid id,
                        [FromBody] Building building,
                        ILogger<BuildingService> logger
                    ) =>
                    {
                        logger.LogInformation("User updates building with ID {id}: {@building}", id, building);

                        var (updatedBuilding, isCreated) = await service.UpdateBuildingAsync(building, id);

                        if (isCreated)
                        {
                            logger.LogInformation("Building with ID {id} was created by user", id);
                            return Results.Created($"/api/v3/assets/buildings/{updatedBuilding.id}", updatedBuilding);
                        }

                        logger.LogInformation("Building with ID {id} was updated by user", id);
                        return Results.Ok(updatedBuilding);
                    }
                )
                .WithName("UpdateBuilding")
                .WithOpenApi()
                .WithTags("Buildings");

            app.MapDelete(
                    "/api/v3/assets/buildings/{id}",
                    async (
                        [FromServices] IBuildingService service,
                        Guid id,
                        ILogger<BuildingService> logger,
                        [FromQuery] bool permanent = false
                    ) =>
                    {
                        var (success, errorMessage) = await service.DeleteBuildingAsync(id, permanent);

                        if (success)
                        {
                            logger.LogInformation("Building with ID {id} successfully deleted", id);
                            return Results.NoContent();
                        }

                        logger.LogWarning("Failed to delete building with ID {id}: {errorMessage}", id, errorMessage);
                        return Results.BadRequest(new { Error = errorMessage });
                    }
                )
                .WithName("DeleteBuilding")
                .WithOpenApi()
                .WithTags("Buildings");
        }
    }
}
