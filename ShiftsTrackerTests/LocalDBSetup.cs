using Microsoft.EntityFrameworkCore;
using ShiftsTrackerRestApi.Managers;

namespace ShiftsTrackerTests;

public static class LocalDBSetup
{
    
    public static UsersManager usersManager;
    public static ShiftsManager shiftsManager;
    
    public static void Setup()
    {
        DbContextOptions<RestContext> options = new DbContextOptionsBuilder<RestContext>()
            .UseInMemoryDatabase(databaseName: "Azure")
            .Options;
        RestContext restContext = new RestContext(options);
        usersManager = new UsersManager(restContext);
        shiftsManager = new ShiftsManager(restContext);
        restContext.Database.EnsureDeleted();
    }

    
}