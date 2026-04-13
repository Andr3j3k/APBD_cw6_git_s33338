using APBD_cw6_git_s33338.Data;
using APBD_cw6_git_s33338.DTOs;
using APBD_cw6_git_s33338.Exceptions;
using APBD_cw6_git_s33338.Interfaces;
using APBD_cw6_git_s33338.Models;

namespace APBD_cw6_git_s33338.Services;

public class RoomService : IRoomService
{
    public IEnumerable<Room> GetAll(int? minCapacity, bool? hasProjector, bool? activeOnly)
    {
        var query = AppData.Rooms.AsEnumerable();
        if (minCapacity.HasValue)
        {
            query=query.Where(r=>r.Capacity>=minCapacity.Value);
        }

        if (hasProjector.HasValue)
        {
            query=query.Where(r=>r.HasProjector==hasProjector.Value);
        }

        if (activeOnly == true)
        {
            query = query.Where(r => r.IsActive);
        }
        return query;
    }

    public Room GetById(int id)
    {
        var room=AppData.Rooms.FirstOrDefault(r=>r.Id == id);
        return room ?? throw new RoomNotFoundException($"Room with id {id} was not found.");
    }

    public IEnumerable<Room> GetByBuilding(string buildingCode)
    {
        return AppData.Rooms.Where(r=>r.BuildingCode.Equals(buildingCode,StringComparison.OrdinalIgnoreCase));
    }

    public Room Add(CreateRoomDto dto)
    {
        var newRoom = new Room
        {
            Id = AppData.Rooms.Count == 0 ? 1 : AppData.Rooms.Max(r => r.Id) + 1,
            Name = dto.Name,
            BuildingCode = dto.BuildingCode,
            Floor = dto.Floor,
            Capacity = dto.Capacity,
            HasProjector = dto.HasProjector,
            IsActive = dto.IsActive
        };
        AppData.Rooms.Add(newRoom);
        
        return newRoom;
    }

    public Room Update(int id, UpdateRoomDto dto)
    {
        var room=AppData.Rooms.FirstOrDefault(r=>r.Id == id);
        if (room is null)
        {
            throw new RoomNotFoundException($"Room with id {id} was not found.");
        }
        room.Name = dto.Name;
        room.BuildingCode = dto.BuildingCode;
        room.Floor = dto.Floor;
        room.Capacity = dto.Capacity;
        room.HasProjector = dto.HasProjector;
        room.IsActive = dto.IsActive;

        return room;
    }

    public void Remove(int id)
    {
        var room=AppData.Rooms.FirstOrDefault(r=>r.Id == id);
        if (room is null)
            throw new RoomNotFoundException($"Room with id {id} was not found.");

        var hasRelatedReservations = AppData.Reservations.Any(r => r.RoomId == id);

        if (hasRelatedReservations)
            throw new RoomDeleteConflictException("Cannot delete room with related reservations.");

        AppData.Rooms.Remove(room);
    }
}