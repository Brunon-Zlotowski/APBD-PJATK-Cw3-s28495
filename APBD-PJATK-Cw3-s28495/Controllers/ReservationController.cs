namespace APBD_PJATK_Cw3_s28495.Controllers;

using Microsoft.AspNetCore.Mvc;
using APBD_PJATK_Cw3_s28495.Data;
using APBD_PJATK_Cw3_s28495.Models;



[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        var reservations = AppData.Reservations.AsQueryable();

        if (date.HasValue)
            reservations = reservations.Where(r => r.Date == date.Value);

        if (!string.IsNullOrWhiteSpace(status))
            reservations = reservations.Where(r => r.Status.ToLower() == status.ToLower());

        if (roomId.HasValue)
            reservations = reservations.Where(r => r.RoomId == roomId.Value);

        return Ok(reservations.ToList());
    }

    [HttpGet("{id}")]
    public ActionResult<Reservation> GetById(int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
            return NotFound();

        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult<Reservation> Create(Reservation reservation)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

        if (room == null)
            return BadRequest("Sala nie istnieje.");

        if (!room.IsActive)
            return BadRequest("Sala jest nieaktywna.");

        bool conflict = AppData.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date &&
            reservation.StartTime < r.EndTime &&
            reservation.EndTime > r.StartTime);

        if (conflict)
            return Conflict("Rezerwacja koliduje czasowo.");

        reservation.Id = AppData.Reservations.Max(r => r.Id) + 1;

        AppData.Reservations.Add(reservation);

        return CreatedAtAction(
            nameof(GetById),
            new { id = reservation.Id },
            reservation);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Reservation updatedReservation)
    {
        var reservation = AppData.Reservations
            .FirstOrDefault(r => r.Id == id);

        if (reservation == null)
            return NotFound();

        bool conflict = AppData.Reservations.Any(r =>
            r.Id != id &&
            r.RoomId == updatedReservation.RoomId &&
            r.Date == updatedReservation.Date &&
            updatedReservation.StartTime < r.EndTime &&
            updatedReservation.EndTime > r.StartTime);

        if (conflict)
            return Conflict("Rezerwacja koliduje czasowo.");

        reservation.RoomId = updatedReservation.RoomId;
        reservation.OrganizerName = updatedReservation.OrganizerName;
        reservation.Topic = updatedReservation.Topic;
        reservation.Date = updatedReservation.Date;
        reservation.StartTime = updatedReservation.StartTime;
        reservation.EndTime = updatedReservation.EndTime;
        reservation.Status = updatedReservation.Status;

        return Ok(reservation);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var reservation = AppData.Reservations
            .FirstOrDefault(r => r.Id == id);

        if (reservation == null)
            return NotFound();

        AppData.Reservations.Remove(reservation);

        return NoContent();
    }
}