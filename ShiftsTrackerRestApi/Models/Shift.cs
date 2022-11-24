using ShiftsTrackerRestApi.Enums;

namespace ShiftsTrackerRestApi.Models;

public class Shift
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }

    public ShiftStatus ShiftStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    
    
}