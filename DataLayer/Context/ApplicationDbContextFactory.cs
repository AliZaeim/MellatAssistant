using DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<MyContext>
{
    public MyContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
        optionsBuilder.UseSqlServer(@"Server=ZAEIMLENEVO\SQLSERVER2019;Database=MellatAssistant_dbV3;Integrated Security=True;Trusted_Connection=True;encrypt=false");

        return new MyContext(optionsBuilder.Options);
    }
}
