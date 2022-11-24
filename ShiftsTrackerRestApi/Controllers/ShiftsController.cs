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
    
    public ShiftsController()
    {
        _usersManager = new UsersManager();
        _shiftsManager = new ShiftsManager();
    }
    
    [HttpGet]
    public IEnumerable<Shift> Get()
    {
        return _shiftsManager.GetShifts();
    }
    

    
}