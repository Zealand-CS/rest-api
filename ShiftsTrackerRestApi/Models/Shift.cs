namespace ShiftsTrackerRestApi.Models;

public class Shift
{
    public string Id { get; set; }
    public string EmployeeId { get; set; }
    public DateTime createdAt { get; set; }
}