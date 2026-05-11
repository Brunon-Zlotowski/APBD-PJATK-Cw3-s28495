namespace APBD_PJATK_Cw3_s28495.Controllers;

using Microsoft.AspNetCore.Mvc;
using APBD_PJATK_Cw3_s28495.Data;
using APBD_PJATK_Cw3_s28495.Models;



[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Room>> GetAll(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        var rooms = AppData.Rooms.AsQueryable();

        if (minCapacity.HasValue)
            rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);

        if (hasProjector.HasValue)
            rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);

        if (activeOnly == true)
            rooms = rooms.Where(r => r.IsActive);

        return Ok(rooms.ToList());
    }

    [HttpGet("{id}")]
    public ActionResult<Room> GetById(int id)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
            return NotFound();

        return Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public ActionResult<IEnumerable<Room>> GetByBuilding(string buildingCode)
    {
        var rooms = AppData.Rooms
            .Where(r => r.BuildingCode.ToLower() == buildingCode.ToLower())
            .ToList();

        return Ok(rooms);
    }

    [HttpPost]
    public ActionResult<Room> Create(Room room)
    {
        room.Id = AppData.Rooms.Max(r => r.Id) + 1;

        AppData.Rooms.Add(room);

        return CreatedAtAction(
            nameof(GetById),
            new { id = room.Id },
            room);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Room updatedRoom)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
            return NotFound();

        room.Name = updatedRoom.Name;
        room.BuildingCode = updatedRoom.BuildingCode;
        room.Floor = updatedRoom.Floor;
        room.Capacity = updatedRoom.Capacity;
        room.HasProjector = updatedRoom.HasProjector;
        room.IsActive = updatedRoom.IsActive;

        return Ok(room);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
            return NotFound();

        bool hasReservations = AppData.Reservations
            .Any(r => r.RoomId == id);

        if (hasReservations)
            return Conflict("Nie można usunąć sali z rezerwacjami.");

        AppData.Rooms.Remove(room);

        return NoContent();
    }
}