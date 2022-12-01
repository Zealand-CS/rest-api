using ShiftsTrackerRestApi.Managers;

namespace ShiftsTrackerTests;


[TestClass]
public class ShiftsManagerTests
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
}