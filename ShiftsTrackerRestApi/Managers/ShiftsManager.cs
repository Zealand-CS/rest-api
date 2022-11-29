using ShiftsTrackerRestApi.Enums;
using ShiftsTrackerRestApi.Models;

namespace ShiftsTrackerRestApi.Managers;

public class ShiftsManager
{

    private RestContext _context;

    public ShiftsManager(RestContext context)
    {
        _context = context;
    }
    
    
    public List<Shift> GetShifts()
    {
        return _context.Shifts.ToList();
    }
    
    public Shift? GetShift(int id)
    {
        return _context.Shifts.FirstOrDefault(s => s.Id == id);
    }
    
    public Shift AddShift(int userId)
    {
        var lastShift = _context.Shifts.OrderBy(s => s.CreatedAt).LastOrDefault(s => s.EmployeeId == userId);
        var shiftStatus = lastShift?.ShiftStatus == ShiftStatus.CheckedIn
            ? ShiftStatus.CheckedOut
            : ShiftStatus.CheckedIn;
        
        var newShift = new Shift
        {
            EmployeeId = userId,
            ShiftStatus = shiftStatus,
            CreatedAt = DateTime.Now
        };
        
        _context.Shifts.Add(newShift);
        _context.SaveChanges();
        return newShift;
    }
    
    public void DeleteShift(int  id)
    {
        var shift = _context.Shifts.FirstOrDefault(s => s.Id == id);
        if (shift == null)
        {
            return;
        }
        
        _context.Shifts.Remove(shift);
        _context.SaveChanges();
    }
    
    public Shift? UpdateShift(int id , Shift shift)
    {
        var existingShift = GetShift(id);
        if (existingShift == null) return null;
        
        
        existingShift.EmployeeId = shift.EmployeeId;
        existingShift.ShiftStatus = shift.ShiftStatus;
        existingShift.CreatedAt = shift.CreatedAt;
        
        _context.SaveChanges();
        
        return existingShift;
    }
    
    public List<Shift> GetShiftsByEmployeeId(int employeeId)
    {
        return _context.Shifts.Where(s => s.EmployeeId == employeeId).ToList();
    }



}