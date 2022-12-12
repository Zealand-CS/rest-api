using System.Data;
using Microsoft.AspNetCore.Mvc;
using ShiftsTrackerRestApi.Managers;
using ShiftsTrackerRestApi.Models;

namespace ShiftsTrackerRestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersManager _usersManager;
   
    
    public UsersController(RestContext context)
    {
        //DB context
        _usersManager = new UsersManager(context);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        var result = _usersManager.GetUsers();
        if (result.Any()) return Ok(result);
        return NoContent();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:int}", Name = "Get")]
    public ActionResult<User?> GetById(int id)
    {
        User? user = _usersManager.GetUser(id);
        if (user == null) return NotFound("The user was not found, id: " + id);
        return Ok(user);
    }
    
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public ActionResult<User> Post([FromBody] User newUser)
    {
        try
        {
            User createdUser = _usersManager.AddUser(newUser);
            return Created("/" + createdUser.Id, createdUser);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id:int}")]
    public ActionResult<User?> Delete(int id)
    {
        User? result = _usersManager.DeleteUser(id);
        if (result == null) return NotFound("The user was not found, id: " + id);
        return Ok(result);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public ActionResult<User> Put(int id, [FromBody] User users)
    {
        try
        {
            User? updatedUser = _usersManager.UpdateUser(id, users);
            if (updatedUser == null) return NotFound();
            
            return Ok(updatedUser);
        }
        
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(ex.Message);
        }
        
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}