using ShiftsTrackerRestApi.Enums;
using ShiftsTrackerRestApi.Utils;

namespace ShiftsTrackerRestApi.Models;

public class Shift
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }

    public ShiftStatus ShiftStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    
    
    public void ValidateEmployeeId()
    {
        if (EmployeeId <= Constants.MIN_EMPLOYEE_ID_LENGTH) throw new ArgumentOutOfRangeException(nameof(EmployeeId),"EmployeeId must be greater than 0");
    }
    
    public void ValidateShiftStatus()
    {
        if (false == Enum.IsDefined(typeof(ShiftStatus), ShiftStatus)) throw new ArgumentException("ShiftStatus is not valid");
    }
    
    public void Validator()
    {
        ValidateEmployeeId();
        ValidateShiftStatus();
    }
    
    
    
    
}