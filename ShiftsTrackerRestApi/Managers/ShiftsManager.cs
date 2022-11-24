using ShiftsTrackerRestApi.Enums;
using ShiftsTrackerRestApi.Models;

namespace ShiftsTrackerRestApi.Managers;

public class ShiftsManager
{
    
    private static int _nextShiftId = 1;


    
    private static readonly List<Shift> _shifts = new ()
    {
        new Shift
        {
            Id = _nextShiftId++,
            EmployeeId = 1,
            ShiftStatus = ShiftStatus.CheckedOut,
            CreatedAt = DateTime.Now
        },
        new Shift
        {
            Id = _nextShiftId++,
            EmployeeId = 2,
            ShiftStatus = ShiftStatus.CheckedIn,
            CreatedAt = DateTime.Now
        },
    };
    
    public List<Shift> GetShifts()
    {
        return _shifts.ToList();
    }
    
    public Shift? GetShift(int id)
    {
        return _shifts.FirstOrDefault(s => s.Id == id);
    }
    
    public Shift AddShift(Shift shift)
    {
        shift.Id = _nextShiftId;
        _nextShiftId++;
        _shifts.Add(shift);
        return shift;
    }
    
    public void DeleteShift(int  id)
    {
        var shift = _shifts.FirstOrDefault(s => s.Id == id);
        if (shift == null)
        {
            return;
        }
        
        _shifts.Remove(shift);
    }
    
    public Shift UpdateShift(int id , Shift shift)
    {
        var existingShift = GetShift(id);
        if (existingShift == null) return null;
        
        
        existingShift.EmployeeId = shift.EmployeeId;
        existingShift.ShiftStatus = shift.ShiftStatus;
        existingShift.CreatedAt = shift.CreatedAt;
        
        return existingShift;
    }



}