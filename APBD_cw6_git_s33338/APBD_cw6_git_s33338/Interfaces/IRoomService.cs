using APBD_cw6_git_s33338.Models;

namespace APBD_cw6_git_s33338.Interfaces;

public interface IRoomService
{
    IEnumerable<Room> GetAll(int? minCapacity, bool? hasProjector, bool? activeOnly);
    Room GetById(int id);
    IEnumerable<Room> GetByBuilding(string buildingCode);
    Room Add(CreateRoomDto dto);
    Room Update(int id, UpdateRoomDto dto);
    void Remove(int id);
}