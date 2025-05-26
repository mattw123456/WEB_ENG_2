using Biletado.Api.Storey;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biletado.Api.Room
{
    public static class ApiRoutes
    {
        public const string Rooms = "/api/v3/assets/rooms";
    }

    public record ApiError(string Message);

    public record RoomCreateDto(string Name, Guid StoreyId);
    public record RoomUpdateDto(string Name, Guid StoreyId);

    public class RoomLogging { }

    public static class RoomEndpunkte
    {
        public static void AddRoomEndpunkte(this IEndpointRouteBuilder app)
        {
            app.MapGet(
                    ApiRoutes.Rooms,
                    GetAllRoomsAsync
                )
                .WithName("GetAllRooms")
                .WithOpenApi()
                .WithTags("Room");

            app.MapPost(
                    ApiRoutes.Rooms,
                    CreateRoomAsync
                )
                .WithName("CreateRoom")
                .WithOpenApi()
                .WithTags("Room")
                .RequireAuthorization();

            app.MapGet(
                    $"{ApiRoutes.Rooms}/{{id}}",
                    GetRoomByIdAsync
                )
                .WithName("GetRoomById")
                .WithOpenApi()
                .WithTags("Room");

            app.MapPut(
                    $"{ApiRoutes.Rooms}/{{id}}",
                    UpdateRoomAsync
                )
                .WithName("UpdateRoom")
                .WithOpenApi()
                .WithTags("Room")
                .RequireAuthorization();

            app.MapDelete(
                    $"{ApiRoutes.Rooms}/{{id}}",
                    DeleteRoomAsync
                )
                .WithName("DeleteRoom")
                .WithOpenApi()
                .WithTags("Room")
                .RequireAuthorization();
        }

        private static async Task<IResult> GetAllRoomsAsync(
            [FromServices] IRoomService service,
            [FromServices] ILogger<RoomLogging> logger,
            [FromQuery] Guid? storey_id,
            [FromQuery] bool include_deleted = false)
        {
            logger.LogInformation("Fetching all rooms (storey_id={StoreyId}, include_deleted={IncludeDeleted})", storey_id, include_deleted);

            var rooms = await service.GetAllRoomsAsync(storey_id, include_deleted);
            return Results.Ok(new { rooms });
        }

        private static async Task<IResult> CreateRoomAsync(
            [FromServices] IStoreyService storeyService,
            [FromServices] IRoomService roomService,
            [FromServices] ILogger<RoomLogging> logger,
            [FromBody] Room room)
        {
            logger.LogInformation("Creating a new room with data: {@Room}", room);

            var storey = await storeyService.GetStoreyByIdAsync(room.storey_id);

            if (storey == null || storey.deleted_at != null)
            {
                logger.LogWarning("Failed to create room: Storey {StoreyId} not found or deleted", room.storey_id);
                return Results.BadRequest(new ApiError("Storey not found or deleted."));
            }

            var createdRoom = await roomService.CreateRoomAsync(room);

            logger.LogInformation("Room successfully created with ID={RoomId}", createdRoom.id);
            return Results.Created($"{ApiRoutes.Rooms}/{createdRoom.id}", createdRoom);
        }

        private static async Task<IResult> GetRoomByIdAsync(
            [FromServices] IRoomService service,
            [FromServices] ILogger<RoomLogging> logger,
            Guid id)
        {
            logger.LogInformation("Fetching room details for ID={RoomId}", id);

            var room = await service.GetRoomByIdAsync(id);
            if (room == null)
            {
                logger.LogWarning("Room with ID={RoomId} not found", id);
                return Results.NotFound(new ApiError("Room not found."));
            }

            logger.LogInformation("Room details fetched for ID={RoomId}", id);
            return Results.Ok(room);
        }

        private static async Task<IResult> UpdateRoomAsync(
            [FromServices] IStoreyService storeyService,
            [FromServices] IRoomService roomService,
            [FromServices] ILogger<RoomLogging> logger,
            Guid id,
            [FromBody] Room room)
        {
            logger.LogInformation("Updating room with ID={RoomId} and data: {@Room}", id, room);

            if (room == null)
                return Results.BadRequest(new ApiError("Room data is required."));

            var storey = await storeyService.GetStoreyByIdAsync(room.storey_id);
            if (storey == null || storey.deleted_at != null)
            {
                logger.LogWarning("Storey {StoreyId} not found or deleted", room.storey_id);
                return Results.BadRequest(new ApiError("Storey not found or deleted."));
            }

            if (room.deleted_at != null)
            {
                logger.LogWarning("Room ID={RoomId} is deleted and cannot be modified", id);
                return Results.BadRequest(new ApiError("Room is deleted and cannot be modified."));
            }

            var updatedRoom = await roomService.UpdateRoomAsync(room, id);
            if (updatedRoom == null)
            {
                logger.LogWarning("Room with ID={RoomId} not found for update", id);
                return Results.NotFound(new ApiError("Room not found or could not be updated."));
            }

            logger.LogInformation("Room with ID={RoomId} successfully updated", id);
            return Results.Ok(updatedRoom);
        }

        private static async Task<IResult> DeleteRoomAsync(
            [FromServices] IRoomService service,
            [FromServices] ILogger<RoomLogging> logger,
            Guid id,
            [FromQuery] bool permanent = false)
        {
            logger.LogInformation("Deleting room with ID={RoomId} (permanent={Permanent})", id, permanent);

            var deleted = await service.DeleteRoomAsync(id, permanent);
            if (deleted)
            {
                logger.LogInformation("Room with ID={RoomId} successfully deleted", id);
                return Results.NoContent();
            }

            logger.LogWarning("Failed to delete room with ID={RoomId}", id);
            return Results.BadRequest(new ApiError("Failed to delete room."));
        }
    }
}