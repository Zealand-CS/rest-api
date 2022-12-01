using Microsoft.EntityFrameworkCore;
using ShiftsTrackerRestApi.Enums;
using ShiftsTrackerRestApi.Managers;
using ShiftsTrackerRestApi.Models;

namespace ShiftsTrackerTests;

[TestClass]
public class UserManagerTests
{
    
    private UsersManager _usersManager;
    

    // Setup DbContext for testing
    [TestInitialize]
    public void Setup()
    {
        LocalDBSetup.Setup();
        _usersManager = LocalDBSetup.usersManager;
    }
    
    [TestMethod]
    public void GetUsersTest()
    {
        var user = new User()
        {
            LastName = "Madisen",
            FirstName = "Thomas",
            Email = "thomasmadisen@shifts.dk",
            NfcCardId = "1234567890",
        };
        
        var user1 = new User()
        {
            LastName = "Patrik",
            FirstName = "Neviem",
            Email = "thomasmadisen@shifts.dk",
            NfcCardId = "1234567890",
        };
        
        _usersManager.AddUser(user);
        _usersManager.AddUser(user1);
        var usersFromDb = _usersManager.GetUsers();
        
        Assert.IsTrue(usersFromDb is { Count: 2 });
        
        var userFromDb = usersFromDb.FirstOrDefault(u => u.Id == user.Id);
        var userFromDb1 = usersFromDb.FirstOrDefault(u => u.Id == user1.Id);
        Assert.AreEqual(user.Id , userFromDb?.Id);
        Assert.AreEqual(user1.Id , userFromDb1?.Id);
    }
    
    [TestMethod]
    public void GetUserTest()
    {
        var user = new User()
        {
            LastName = "Madisen",
            FirstName = "Thomas",
            Email = "thomasmadisen@shifts.dk",
            NfcCardId = "1234567890",
        };
        
        _usersManager.AddUser(user);
        var userFromDb = _usersManager.GetUser(user.Id);
        Assert.IsTrue(userFromDb != null && userFromDb.Id == user.Id);
        Assert.AreEqual( user.FirstName, userFromDb.FirstName);
    }
    
    [TestMethod]
    public void AddUserTest()
    {
        var user = new User()
        {
            LastName = "Madisen",
            FirstName = "Thomas",
            Email = "thomasmadisen@shifts.dk",
            NfcCardId = "1234567890",
        };
        
        var result = _usersManager.AddUser(user);
        
        Assert.AreEqual("Thomas", result.FirstName);
        Assert.IsTrue(_usersManager.GetUser(result.Id) != null);
        Assert.IsFalse(_usersManager.GetUser(result.Id)?.LastName == "Mad");
    }

    [TestMethod]
    public void UpdateUserTest()
    {
        var primaryLastName = "Madisen";
        var user = new User()
        {
            LastName = primaryLastName,
            FirstName = "Thomas",
            Email = "thomasmadisen@shifts.dk",
            NfcCardId = "1234567890",
        };
        
        var result = _usersManager.AddUser(user);
        Assert.IsTrue(user.LastName == result.LastName);
        
        var updatedLastName = "Madison";
        result.LastName = updatedLastName;
        var result2 = _usersManager.UpdateUser(result.Id, result);
        
        if (result2 == null) Assert.Fail();
        
        Assert.AreEqual("Thomas", result2.FirstName);
        Assert.IsFalse(result2.LastName == primaryLastName);
        Assert.IsTrue(result2.LastName == updatedLastName);
    }

    [TestMethod]
    public void DeleteUserTest()
    {
        var user = new User()
        {
            LastName = "Madisen",
            FirstName = "Thomas",
            Email = "thomasmadisen@shifts.dk",
            NfcCardId = "1234567890",
        };
        
        var addedUser = _usersManager.AddUser(user);
        
        var userFromDb = _usersManager.GetUser(addedUser.Id);
        Assert.IsTrue(_usersManager.GetUser(addedUser.Id) != null);
        Assert.IsTrue(userFromDb != null && userFromDb.Id == addedUser.Id);

        _usersManager.DeleteUser(addedUser.Id);
        Assert.IsTrue(_usersManager.GetUser(addedUser.Id) == null);
    }

    [TestMethod]
    public void GetUserByNfcCardIdTest()
    {
        var user = new User()
        {
            LastName = "Madisen",
            FirstName = "Thomas",
            Email = "thomasmadisen@shifts.dk",
            NfcCardId = "1a2b3c4d5e6f7g8h9i0j",
        };
        
        _usersManager.AddUser(user);
        var userFromDb = _usersManager.GetUserByNfcCardId(user.NfcCardId);
        
        Assert.AreEqual(user.NfcCardId, userFromDb?.NfcCardId);
        Assert.IsFalse(userFromDb?.NfcCardId == "1a2b3c4d5e6f7");
    }
}