using APBD_cw6_git_s33338.Models;

namespace APBD_cw6_git_s33338.Interfaces;

public interface IReservationService
{
    IEnumerable<Reservation> GetAll(DateOnly? date, string? status, int? roomId);
    Reservation GetById(int id);
    Reservation Add(CreateReservationDto dto);
    Reservation Update(int id, UpdateReservationDto dto);
    void Remove(int id);
}