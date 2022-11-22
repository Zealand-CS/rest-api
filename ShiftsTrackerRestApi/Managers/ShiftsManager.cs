using ShiftsTrackerRestApi.Models;

namespace ShiftsTrackerRestApi.Managers;

public class ShiftsManager
{
    
    private static int _nextUserId = 1;
    private static int _nextShiftId = 1;


    private static List<User> _users = new List<User>();
    private static List<Shift> _shifts = new List<Shift>();
    
    public List<User> GetUsers()
    {
        return _users.ToList();
    }
    
    public User? GetUser(string id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }
    
    public User AddUser(User user)
    {
        user.Id = _nextUserId.ToString();
        _nextUserId++;
        _users.Add(user);
        return user;
    }
    
    public User UpdateUser(User user)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser == null)
        {
            return null;
        }
        
        existingUser.NfcCardId = user.NfcCardId;
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Email = user.Email;
        existingUser.Role = user.Role;
        
        return existingUser;
    }
    
    public void DeleteUser(string id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return;
        }
        
        _users.Remove(user);
    }
    
    
    
    public List<Shift> GetShifts()
    {
        return _shifts.ToList();
    }
    
    public Shift? GetShift(string id)
    {
        return _shifts.FirstOrDefault(s => s.Id == id);
    }
    
    public Shift AddShift(Shift shift)
    {
        shift.Id = _nextShiftId.ToString();
        _nextShiftId++;
        _shifts.Add(shift);
        return shift;
    }
    
    public void DeleteShift(string id)
    {
        var shift = _shifts.FirstOrDefault(s => s.Id == id);
        if (shift == null)
        {
            return;
        }
        
        _shifts.Remove(shift);
    }



}