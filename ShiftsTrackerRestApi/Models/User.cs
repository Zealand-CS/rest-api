using ShiftsTrackerRestApi.Enums;

namespace ShiftsTrackerRestApi.Models;

public class User
{
    public string Id { get; set; }
    public string NfcCardId { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
}