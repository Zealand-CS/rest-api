using ShiftsTrackerRestApi.Enums;

namespace ShiftsTrackerRestApi.Models;

public class Shift
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }

    public ShiftStatus ShiftStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    
    
    public void ValidateEmployeeId()
    {
        if (EmployeeId <= 0) throw new ArgumentOutOfRangeException(nameof(EmployeeId),"EmployeeId must be greater than 0");
    }
    
    public void ValidateShiftStatus()
    {
        if (false == Enum.IsDefined(typeof(ShiftStatus), ShiftStatus)) throw new ArgumentException("Role is not valid");
    }
    
    
    
    
}