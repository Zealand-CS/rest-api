using Microsoft.AspNetCore.Mvc;
using ShiftsTrackerRestApi.Managers;
using ShiftsTrackerRestApi.Models;

namespace ShiftsTrackerRestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ShiftController : ControllerBase
{
    
    private readonly ShiftsManager _shiftsManager;
    
    public ShiftController()
    {
        _shiftsManager = new ShiftsManager();
    }
    
    [HttpGet]
    public IEnumerable<Shift> Get()
    {
        return _shiftsManager.GetShifts();
    }
    

    
}