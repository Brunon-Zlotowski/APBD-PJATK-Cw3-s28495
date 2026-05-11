namespace APBD_PJATK_Cw3_s28495.Data;

using APBD_PJATK_Cw3_s28495.Models;



public static class AppData
{
    public static List<Room> Rooms = new()
    {
        new Room
        {
            Id = 1,
            Name = "Room A101",
            BuildingCode = "A",
            Floor = 1,
            Capacity = 20,
            HasProjector = true,
            IsActive = true
        },

        new Room
        {
            Id = 2,
            Name = "Room A102",
            BuildingCode = "A",
            Floor = 1,
            Capacity = 15,
            HasProjector = false,
            IsActive = true
        },

        new Room
        {
            Id = 3,
            Name = "Lab B201",
            BuildingCode = "B",
            Floor = 2,
            Capacity = 30,
            HasProjector = true,
            IsActive = true
        },

        new Room
        {
            Id = 4,
            Name = "Conference C301",
            BuildingCode = "C",
            Floor = 3,
            Capacity = 50,
            HasProjector = true,
            IsActive = false
        },

        new Room
        {
            Id = 5,
            Name = "Training Room B105",
            BuildingCode = "B",
            Floor = 1,
            Capacity = 25,
            HasProjector = false,
            IsActive = true
        }
    };

    public static List<Reservation> Reservations = new()
    {
        new Reservation
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Anna Kowalska",
            Topic = "ASP.NET Core",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(12, 0),
            Status = "confirmed"
        },

        new Reservation
        {
            Id = 2,
            RoomId = 3,
            OrganizerName = "Jan Nowak",
            Topic = "REST API",
            Date = new DateOnly(2026, 5, 11),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(11, 0),
            Status = "planned"
        },

        new Reservation
        {
            Id = 3,
            RoomId = 2,
            OrganizerName = "Maria Zielinska",
            Topic = "Docker",
            Date = new DateOnly(2026, 5, 12),
            StartTime = new TimeOnly(13, 0),
            EndTime = new TimeOnly(15, 0),
            Status = "confirmed"
        },

        new Reservation
        {
            Id = 4,
            RoomId = 5,
            OrganizerName = "Piotr Adamski",
            Topic = "Git Workshop",
            Date = new DateOnly(2026, 5, 13),
            StartTime = new TimeOnly(8, 30),
            EndTime = new TimeOnly(10, 30),
            Status = "cancelled"
        }
    };
}