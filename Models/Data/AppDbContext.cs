using Microsoft.EntityFrameworkCore;
#nullable disable

public class AppDbContext : DbContext {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=localhost\MSSQLSERVER01;
                                    Initial Catalog = MyStoreDB;Integrated Security=True;
                                    Connect Timeout=30;
                                    Encrypt=False;
                                    Trust Server Certificate=False;
                                    Application Intent=ReadWrite;
                                    Multi Subnet Failover=False"
        );
    }

    public DbSet<Client> clients {set; get;}
}