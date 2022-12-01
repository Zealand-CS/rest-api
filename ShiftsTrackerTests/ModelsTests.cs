using System.ComponentModel.DataAnnotations;
using ShiftsTrackerRestApi.Enums;
using ShiftsTrackerRestApi.Managers;
using ShiftsTrackerRestApi.Models;

namespace ShiftsTrackerTests;

[TestClass]
public class ModelsTests
{
    private User _user;
    private Shift _shift;
    
    
    [TestInitialize]
    public void Setup()
    {
        _user = new User()
        {
            LastName = "Test",
            FirstName = "Test",
            Email = "text@test.dk",
            NfcCardId = "1234567890",
        };
        
        _shift = new Shift()
        {
            EmployeeId = 1,
            CreatedAt = DateTime.Now
        };
    }
    
    [TestMethod]
    public void FirstNameValidationTest()
    {
        _user.FirstNameValidation();
        
        // Null or empty checks
        _user.FirstName = "";
        Assert.ThrowsException<ArgumentNullException>(() => _user.FirstNameValidation());
        _user.FirstName = null;
        Assert.ThrowsException<ArgumentNullException>(() => _user.FirstNameValidation());
        
        // Length checks
        _user.FirstName = "TestTestTestTestTestTestTestTestTest";
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _user.FirstNameValidation());
        _user.FirstName = "Te";
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _user.FirstNameValidation());
        
        // IsDigit checks
        _user.FirstName = "Test123";
        Assert.ThrowsException<ValidationException>(() => _user.FirstNameValidation());
        
        // IsPunctuation checks
        _user.FirstName = "Test,";
        Assert.ThrowsException<ValidationException>(() => _user.FirstNameValidation());
    }
    
    [TestMethod]
    public void LastNameValidationTest()
    {
        _user.LastNameValidation();
        // Null or empty checks
        _user.LastName = "";
        Assert.ThrowsException<ArgumentNullException>(() => _user.LastNameValidation());
        _user.LastName = null;
        Assert.ThrowsException<ArgumentNullException>(() => _user.LastNameValidation());
        
        // Length checks
        _user.LastName = "TestLastNameTestLastNameTestLastNameTestLastName";
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _user.LastNameValidation());
        _user.LastName = "La";
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _user.LastNameValidation());
        
        // IsDigit checks
        _user.LastName = "TestLastName123";
        Assert.ThrowsException<ValidationException>(() => _user.LastNameValidation());
        
        // IsPunctuation checks
        _user.LastName = "TestLastName,";
        Assert.ThrowsException<ValidationException>(() => _user.LastNameValidation());
    }

    [TestMethod]
    public void EmailValidationTests()
    {
        _user.EmailValidation();
        
       //Regex checks
        _user.Email = "test@test";
        Assert.ThrowsException<ArgumentException>(() => _user.EmailValidation());
        _user.Email = "test@test.dk?";
        Assert.ThrowsException<ArgumentException>(() => _user.EmailValidation());
        _user.Email = "test.@test.dk";
        Assert.ThrowsException<ArgumentException>(() => _user.EmailValidation());
        
        // Null or empty checks
        _user.Email = "";
        Assert.ThrowsException<ArgumentNullException>(() => _user.EmailValidation());
        _user.Email = null;
        Assert.ThrowsException<ArgumentNullException>(() => _user.EmailValidation());
        
    }

    [TestMethod]
    public void RoleValidationCheck()
    {
        _user.RoleValidation();

        var  role = (Role) (3);
        _user.Role = role;
        Assert.ThrowsException<ArgumentException>(() => _user.RoleValidation());
    }
    
    [TestMethod]
    public void ShiftStatusValidationTest()
    {
        _shift.ValidateShiftStatus();
        
        var shiftStatus = (ShiftStatus) (3);
        _shift.ShiftStatus = shiftStatus;
        Assert.ThrowsException<ArgumentException>(() => _shift.ValidateShiftStatus());
    }
    
    [TestMethod]
    public void ShiftEmployeeIdValidationTest()
    {
        _shift.ValidateEmployeeId();
        
        _shift.EmployeeId = 0;
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _shift.ValidateEmployeeId());
    }

}