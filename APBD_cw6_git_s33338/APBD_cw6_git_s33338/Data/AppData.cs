using APBD_cw6_git_s33338.Models;

namespace APBD_cw6_git_s33338.Data;

public class AppData
{
    public static List<Room> Rooms { get; set; } = new List<Room>
        {
            new Room
            {
                Id = 1,
                Name = "Lab 101",
                BuildingCode = "A",
                Floor = 1,
                Capacity = 20,
                HasProjector = true,
                IsActive = true
            },
            new Room
            {
                Id = 2,
                Name = "Lab 204",
                BuildingCode = "B",
                Floor = 2,
                Capacity = 24,
                HasProjector = true,
                IsActive = true
            },
            new Room
            {
                Id = 3,
                Name = "Sala Konferencyjna",
                BuildingCode = "A",
                Floor = 3,
                Capacity = 12,
                HasProjector = false,
                IsActive = true
            },
            new Room
            {
                Id = 4,
                Name = "Warsztat 12",
                BuildingCode = "C",
                Floor = 1,
                Capacity = 30,
                HasProjector = true,
                IsActive = false
            },
            new Room
            {
                Id = 5,
                Name = "Sala 55",
                BuildingCode = "B",
                Floor = 5,
                Capacity = 18,
                HasProjector = false,
                IsActive = true
            }
        };

        public static List<Reservation> Reservations { get; set; } = new List<Reservation>
        {
            new Reservation
            {
                Id = 1,
                RoomId = 2,
                OrganizerName = "Anna Kowalska",
                Topic = "Warsztaty z HTTP i REST",
                Date = new DateOnly(2026, 5, 10),
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(12, 30),
                Status = "confirmed"
            },
            new Reservation
            {
                Id = 2,
                RoomId = 1,
                OrganizerName = "Jan Nowak",
                Topic = "Konsultacje C#",
                Date = new DateOnly(2026, 5, 11),
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(10, 0),
                Status = "planned"
            },
            new Reservation
            {
                Id = 3,
                RoomId = 3,
                OrganizerName = "Ewa Zielińska",
                Topic = "Szkolenie ASP.NET",
                Date = new DateOnly(2026, 5, 12),
                StartTime = new TimeOnly(13, 0),
                EndTime = new TimeOnly(15, 0),
                Status = "confirmed"
            },
            new Reservation
            {
                Id = 4,
                RoomId = 5,
                OrganizerName = "Piotr Wiśniewski",
                Topic = "Spotkanie organizacyjne",
                Date = new DateOnly(2026, 5, 13),
                StartTime = new TimeOnly(8, 30),
                EndTime = new TimeOnly(9, 30),
                Status = "cancelled"
            }
        };
}