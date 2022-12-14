using Microsoft.AspNetCore.Mvc;
using ShiftsTrackerRestApi.Managers;
using ShiftsTrackerRestApi.Models;

namespace ShiftsTrackerRestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ShiftsController : ControllerBase
{
    
    private readonly ShiftsManager _shiftsManager;
    private readonly UsersManager _usersManager;
    
    public ShiftsController(RestContext context)
    {
        _usersManager = new UsersManager(context);
        _shiftsManager = new ShiftsManager(context);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<IEnumerable<Shift>> GetAll()
    {
        var result = _shiftsManager.GetShifts();
        if (result.Any()) return Ok(result);
        return NoContent();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:int}", Name = "Get Shift by Id")]
    public ActionResult<Shift?> GetById(int id)
    {
        Shift? result = _shiftsManager.GetShift(id);
        if (result == null) return NotFound("The shift was not found, id: " + id);
        return Ok(result);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("user/{id:int}", Name = "Get Shifts by User Id")]
    public ActionResult<IEnumerable<Shift>> GetByUserId(int id)
    {
        var result = _shiftsManager.GetShiftsByEmployeeId(id);
        if (result.Any()) return Ok(result);
        return NoContent();
    }
    
    // POST method for adding a new shift. We need to extract nfcCardId from body
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public ActionResult<Shift> Create([FromBody] ShiftDataProtocol? data)
    {
        if (data == null) return BadRequest("Nfc card id is null");
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = _usersManager.GetUserByNfcCardId(data.NfcCardId);
        if (user == null) return BadRequest("User not found");
        var result = _shiftsManager.AddShift(user.Id);
        return CreatedAtRoute("Get Shift by Id", new { id = result.Id }, result);
    }
}