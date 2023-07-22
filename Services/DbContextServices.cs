public class DbContextServices {
    private AppDbContext _dbContext;

    public DbContextServices(AppDbContext dbContext) {
        _dbContext = dbContext;
    }

    public void InitData() {
        _dbContext.clients.AddRange(new List<Client>() {
            new Client("Bill Gates", "bill.gates@gmail.com", "+123456789", "New York, USA"),
            new Client("Elon Musk", "elon.musk@gmail.com", "+123456789", "New York, USA"),
            new Client("Will Smith", "will.smith@gmail.com", "+123456789", "New York, USA"),
            new Client("Cristiano Ronaldo", "cr7.ronaldo@gmail.com", "+123456789", "New York, USA"),
            new Client("Leo Messi", "leo.messi@gmail.com", "+123456789", "New York, USA"),
        });
        _dbContext.SaveChanges();
    }

    public bool CreateDb() {
        var isCreated = this._dbContext.Database.EnsureCreated();
        // InitData();
        // var init = _dbContext.SaveChanges();
        return isCreated;
    }

    public bool DeleteDb() {
        var isDeleted = this._dbContext.Database.EnsureDeleted();
        return isDeleted;
    }

    public List<Client> ShowAllClients() {
        return _dbContext.clients.ToList();
    }
}