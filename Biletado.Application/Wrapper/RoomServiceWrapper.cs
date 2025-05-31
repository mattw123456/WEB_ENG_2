using Biletado.Domain.Room;

namespace Biletado.Application.Wrapper
{
    public class RoomServiceWrapper : IRoomService
    {
        private readonly IRoomService _wrappedService;

        public RoomServiceWrapper(IRoomService service)
        {
            _wrappedService = service;
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            Console.WriteLine("[Vom Wrapper]: Erstelle neuen Raum");
            return await _wrappedService.CreateRoomAsync(room);
        }

        public async Task<bool> DeleteRoomAsync(Guid id, bool permanent = false)
        {
            Console.WriteLine("[Vom Wrapper]: Lösche Raum");
            return await _wrappedService.DeleteRoomAsync(id, permanent);
        }

        public async Task<List<Room>> GetAllRoomsAsync(Guid? storey_id = null, bool includeDeleted = false)
        {
            Console.WriteLine("[Vom Wrapper]: Rufe alle Räume von Storey ab");
            return await _wrappedService.GetAllRoomsAsync(storey_id ,includeDeleted);
        }

        public async Task<Room> GetRoomByIdAsync(Guid id)
        {
            Console.WriteLine($"[Vom Wrapper]: Rufe Raum mit ID: {id} ab");
            return await _wrappedService.GetRoomByIdAsync(id);
        }

        public async Task<Room> UpdateRoomAsync(Room room, Guid id)
        {
            Console.WriteLine($"[Vom Wrapper]: Update Raum mit id {id}");
            return await _wrappedService.UpdateRoomAsync(room, id);
        }
    }
}
