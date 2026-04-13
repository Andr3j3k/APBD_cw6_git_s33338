using APBD_cw6_git_s33338.DTOs;
using APBD_cw6_git_s33338.Exceptions;
using APBD_cw6_git_s33338.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APBD_cw6_git_s33338.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(IRoomService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        return Ok(service.GetAll(minCapacity, hasProjector, activeOnly));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        try
        {
            return Ok(service.GetById(id));
        }
        catch (RoomNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("building/{buildingCode}")]
    public IActionResult GetByBuilding([FromRoute] string buildingCode)
    {
        return Ok(service.GetByBuilding(buildingCode));
    }

    [HttpPost]
    public IActionResult Add([FromBody] CreateRoomDto room)
    {
        var createdRoom=service.Add(room);
        
        return CreatedAtAction(
            nameof(GetById),
            new {id=createdRoom.Id},
            createdRoom);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateRoomDto room)
    {
        try
        {
            return Ok(service.Update(id, room));
        }
        catch (RoomNotFoundException e)
        {
            return NotFound(e.Message);
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
        catch (RoomNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (RoomDeleteConflictException e)
        {
            return Conflict(e.Message);
        }
    }
}
