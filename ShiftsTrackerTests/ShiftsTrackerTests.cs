using Microsoft.EntityFrameworkCore;
using ShiftsTrackerRestApi.Enums;
using ShiftsTrackerRestApi.Managers;
using ShiftsTrackerRestApi.Models;

namespace ShiftsTrackerTests;

[TestClass]
public class ShiftsTrackerTests
{
    private DbContextOptions<RestContext> _options;
    private RestContext _context;
    private UsersManager _usersManager;
    private ShiftsManager _shiftsManager;


    // Setup DbContext for testing
    [TestInitialize]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<RestContext>()
            .UseInMemoryDatabase(databaseName: "Azure")
            .Options;
        _context = new RestContext(_options);
        _usersManager = new UsersManager(_context);
        _shiftsManager = new ShiftsManager(_context);
    }


    [TestMethod]
    public void AddUserTest()
    {
        var user = new User()
        {
            LastName = "Test",
            FirstName = "Test",
            Email = "text@test.dk",
            NfcCardId = "1234567890",
        };
        var result = _usersManager.AddUser(user);
        Assert.AreEqual("Test", result.LastName);
  
    }
}