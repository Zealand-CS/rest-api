using Microsoft.EntityFrameworkCore;
using ShiftsTrackerRestApi.Enums;
using ShiftsTrackerRestApi.Managers;
using ShiftsTrackerRestApi.Models;

namespace ShiftsTrackerTests;

[TestClass]
public class ShiftsTrackerTests
{
    
    private UsersManager _usersManager;
    private ShiftsManager _shiftsManager;
    
    


    // Setup DbContext for testing
    [TestInitialize]
    public void Setup()
    {
        LocalDBSetup.Setup();
        _usersManager = LocalDBSetup.usersManager;
        _shiftsManager = LocalDBSetup.shiftsManager;
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

    [TestMethod]
    public void UpdateUserTest()
    {
        var user = new User()
        {
            LastName = "Test",
            FirstName = "Test",
            Email = "text@test.dk",
            NfcCardId = "1234567890",
        };
        var result = _usersManager.AddUser(user);
        
        result.LastName = "TestTwo";
        var result2 = _usersManager.UpdateUser(result.Id, result);
        if (result2 == null)
        {
            Assert.Fail();
        }

        Assert.AreEqual("TestTwo", result2.LastName);
    }
}