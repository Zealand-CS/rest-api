using ShiftsTrackerRestApi.Enums;
using ShiftsTrackerRestApi.Models;

namespace ShiftsTrackerRestApi.Managers;

public class UsersManager
{
    private RestContext _context;

    public UsersManager(RestContext context)
    {
        _context = context;
    }
    
    public List<User> GetUsers()
    {
        return _context.Users.ToList();
    }
    
    public User? GetUser(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }
    
    public User AddUser(User user)
    {
        user.Role = Role.Employee;
        user.Validator();
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }
    
    public User? DeleteUser(int id)
    {
        //User? user = _context.Users.FirstOrDefault(u => u.Id == id);
        User? user = _context.Users.Find(id);
        if (user == null) return null;
        _context.Users.Remove(user);
        _context.SaveChanges();
        return user;
    }

    public User? UpdateUser(int id, User user)
    {
        var existingUser = GetUser(id);
        if (existingUser == null) return null;
        
        existingUser.NfcCardId = user.NfcCardId;
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Email = user.Email;
        
        user.Validator();
        _context.SaveChanges();
        
        return existingUser;
    }
    
    public User? GetUserByNfcCardId(string nfcCardId)
    {
        return _context.Users.FirstOrDefault(u => u.NfcCardId == nfcCardId);
    }
    
}