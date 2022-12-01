using Microsoft.EntityFrameworkCore;
using ShiftsTrackerRestApi.Enums;
using ShiftsTrackerRestApi.Managers;
using ShiftsTrackerRestApi.Models;

namespace ShiftsTrackerTests;


[TestClass]
public class ShiftsManagerTests
{
    private UsersManager usersManager;
    private ShiftsManager shiftsManager;
    
    // Setup DbContext for testing
    [TestInitialize]
    public void Setup()
    {
        
        LocalDBSetup.Setup();
        usersManager = LocalDBSetup.usersManager;
        shiftsManager = LocalDBSetup.shiftsManager;
        
        var user = new User()
        {
            LastName = "Madisen",
            FirstName = "Thomas",
            Email = "thomasmadisen@shifts.dk",
            NfcCardId = "a8b3aklae12",
        };
        
        var user1 = new User()
        {
            LastName = "Madisen",
            FirstName = "Gustav",
            Email = "gustavmadisen@shifts.dk",
            NfcCardId = "a8b3aklae13",
        };
        
        usersManager.AddUser(user);
        usersManager.AddUser(user1);
    }
    
    [TestMethod]
    public void TestCreateShift()
    {
        var userToCreateShiftFor = usersManager.GetUser(1);
        
        var addedShift = shiftsManager.AddShift(userToCreateShiftFor!.Id);
       
        Assert.IsNotNull(addedShift);
        Assert.AreEqual(ShiftStatus.CheckedIn, addedShift.ShiftStatus);
        Assert.IsTrue(usersManager.GetUser(addedShift.Id) != null);
        
        var addedShiftToCheckOut = shiftsManager.AddShift(userToCreateShiftFor.Id);
        Assert.AreEqual(ShiftStatus.CheckedOut, addedShiftToCheckOut.ShiftStatus);
    }
    
    [TestMethod]
    public void TestGetShifts()
    {
        var userToCreateShiftForId1 = usersManager.GetUser(1);
        var userToCreateShiftForId2 = usersManager.GetUser(2);
        
        shiftsManager.AddShift(userToCreateShiftForId1!.Id);
        shiftsManager.AddShift(userToCreateShiftForId1.Id);
        shiftsManager.AddShift(userToCreateShiftForId2!.Id);
        
        var shifts = shiftsManager.GetShifts();
        
        Assert.IsTrue(shifts  is { Count: 3 });
        
        var shiftFromDb = shiftsManager.GetShift(1);
        Assert.AreEqual(ShiftStatus.CheckedIn, shiftFromDb!.ShiftStatus);
        var shiftFromDb1 = shiftsManager.GetShift(2);
        Assert.AreEqual(ShiftStatus.CheckedOut, shiftFromDb1!.ShiftStatus);
        var shiftFromDb2 = shiftsManager.GetShift(3);
        Assert.IsTrue(shiftFromDb2?.EmployeeId == 2);
    }

    [TestMethod]
    public void GetShiftTest()
    {
        var userToCreateShiftFor = usersManager.GetUser(2);
        
        shiftsManager.AddShift(userToCreateShiftFor!.Id);

        var returnedShift = shiftsManager.GetShift(1);
        Assert.IsNotNull(returnedShift);
        Assert.AreEqual(ShiftStatus.CheckedIn, returnedShift!.ShiftStatus);
        Assert.IsTrue(returnedShift.EmployeeId == 2);
    }

    [TestMethod]
    public void GetShiftsByUserIdTest()
    {
        var userToCreateShiftForId1 = usersManager.GetUser(1);
        var userToCreateShiftForId2 = usersManager.GetUser(2);

        shiftsManager.AddShift(userToCreateShiftForId1!.Id);
        shiftsManager.AddShift(userToCreateShiftForId1.Id);
        shiftsManager.AddShift(userToCreateShiftForId1.Id);
        shiftsManager.AddShift(userToCreateShiftForId2!.Id);

        var shifts = shiftsManager.GetShiftsByEmployeeId(userToCreateShiftForId1.Id);

        var shiftFromDb = shiftsManager.GetShift(3);
        var shiftFromDb1 = shiftsManager.GetShift(4);

        Assert.IsTrue(shifts is { Count: 3 });
        Assert.AreEqual(ShiftStatus.CheckedIn, shiftFromDb!.ShiftStatus);
        Assert.IsTrue(shiftFromDb1?.EmployeeId == 2 );
    }

}