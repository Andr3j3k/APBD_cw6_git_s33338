using APBD_cw6_git_s33338.DTOs;
using APBD_cw6_git_s33338.Exceptions;
using APBD_cw6_git_s33338.Interfaces;
using APBD_cw6_git_s33338.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_cw6_git_s33338.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController(IReservationService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        return Ok(service.GetAll(date, status, roomId));
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        try
        {
            return Ok(service.GetById(id));
        }
        catch (ReservationNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public IActionResult Add([FromBody] CreateReservationDto reservation)
    {
        try
        {
            var createdReservation=service.Add(reservation);
            
            return CreatedAtAction(
                nameof(GetById),
                new{id=createdReservation.Id},
                createdReservation);
        }
        catch(RoomNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch(InactiveRoomReservationException e)
        {
            return Conflict(e.Message);
        }
        catch(ReservationConflictException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateReservationDto reservation)
    {
        try
        {
            return Ok(service.Update(id, reservation));
        }
        catch(ReservationNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch(RoomNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch(InactiveRoomReservationException e)
        {
            return Conflict(e.Message);
        }
        catch(ReservationConflictException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        try
        {
            service.Remove(id);
            return NoContent();
        }
        catch(ReservationNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}