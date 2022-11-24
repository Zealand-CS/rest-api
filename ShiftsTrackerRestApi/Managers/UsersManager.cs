using ShiftsTrackerRestApi.Enums;
using ShiftsTrackerRestApi.Models;

namespace ShiftsTrackerRestApi.Managers;

public class UsersManager
{
    private static int _nextUserId = 1;
    
    private RestContext _context;

    public UsersManager(RestContext context)
    {
        _context = context;
    }
    
    
    /*
    private static List<User> _users = new()
    {
        new User
        {
            Email = "email@email.com",
            Id = _nextUserId++,
            FirstName = "John",
            LastName = "Doe",
            NfcCardId = "1b",
            Role = Role.Admin
        },
        
        new User
        {
            Email = "email@email.com",
            Id = _nextUserId++,
            FirstName = "Jane",
            LastName = "Frraau",
            NfcCardId = "1a",
            Role = Role.Employee
        },
    };
    */
    
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
        user.Id = _nextUserId;
        user.Role = Role.Employee;
        _nextUserId++;
        _context.Users.Add(user);
        return user;
    }
    
    
    public User? DeleteUser(int id)
    {
        //User? user = _context.Users.FirstOrDefault(u => u.Id == id);
        User? user = _context.Users.Find(id);
        if (user == null) return null;
        _context.Users.Remove(user);
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
        
        return existingUser;
    }
    
    public User? GetUserByNfcCardId(string nfcCardId)
    {
        return _context.Users.FirstOrDefault(u => u.NfcCardId == nfcCardId);
    }
    
}