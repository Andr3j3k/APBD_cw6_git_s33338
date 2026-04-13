using APBD_cw6_git_s33338.Data;
using APBD_cw6_git_s33338.DTOs;
using APBD_cw6_git_s33338.Exceptions;
using APBD_cw6_git_s33338.Interfaces;
using APBD_cw6_git_s33338.Models;

namespace APBD_cw6_git_s33338.Services;

public class ReservationService : IReservationService
{
    public IEnumerable<Reservation> GetAll(DateOnly? date, string? status, int? roomId)
    {
        var query = AppData.Reservations.AsEnumerable();

        if (date.HasValue)
        {
            query = query.Where(r => r.Date == date.Value);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        if (roomId.HasValue)
        {
            query = query.Where(r => r.RoomId == roomId.Value);    
        }
        
        return query;
    }

    public Reservation GetById(int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        return reservation ?? throw new ReservationNotFoundException($"Reservation with id {id} was not found.");
    }

    public Reservation Add(CreateReservationDto dto)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == dto.RoomId);

        if (room is null)
        {
            throw new RoomNotFoundException($"Room with id {dto.RoomId} was not found.");
        }

        if (!room.IsActive)
        {
            throw new InactiveRoomReservationException("Cannot add reservation for inactive room.");
        }

        if (HasConflict(dto.RoomId, dto.Date, dto.StartTime, dto.EndTime))
        {
            throw new ReservationConflictException("Reservation time conflicts with an existing reservation.");   
        }

        var newReservation = new Reservation
        {
            Id = AppData.Reservations.Count == 0 ? 1 : AppData.Reservations.Max(r => r.Id) + 1,
            RoomId = dto.RoomId,
            OrganizerName = dto.OrganizerName,
            Topic = dto.Topic,
            Date = dto.Date,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Status = dto.Status
        };

        AppData.Reservations.Add(newReservation);

        return newReservation;
    }

    public Reservation Update(int id, UpdateReservationDto dto)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation is null)
        {
            throw new ReservationNotFoundException($"Reservation with id {id} was not found.");
        }

        var room = AppData.Rooms.FirstOrDefault(r => r.Id == dto.RoomId);

        if (room is null)
        {
            throw new RoomNotFoundException($"Room with id {dto.RoomId} was not found.");   
        }

        if (!room.IsActive)
        {
            throw new InactiveRoomReservationException("Cannot assign reservation to inactive room.");   
        }

        if (HasConflict(dto.RoomId, dto.Date, dto.StartTime, dto.EndTime, id))
        {
            throw new ReservationConflictException("Reservation time conflicts with an existing reservation.");   
        }

        reservation.RoomId = dto.RoomId;
        reservation.OrganizerName = dto.OrganizerName;
        reservation.Topic = dto.Topic;
        reservation.Date = dto.Date;
        reservation.StartTime = dto.StartTime;
        reservation.EndTime = dto.EndTime;
        reservation.Status = dto.Status;

        return reservation;
    }

    public void Remove(int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation is null)
        {
            throw new ReservationNotFoundException($"Reservation with id {id} was not found.");   
        }

        AppData.Reservations.Remove(reservation);
    }

    private static bool HasConflict(
        int roomId,
        DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime,
        int? ignoredReservationId = null
        )
    {
        return AppData.Reservations.Any(r =>
            r.RoomId == roomId &&
            r.Date == date &&
            r.Id != ignoredReservationId &&
            startTime < r.EndTime &&
            endTime > r.StartTime
            );
    }
}