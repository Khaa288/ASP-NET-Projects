namespace Practice_ASP_NET.Services;

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
        // if (CreateDb())
        _dbContext.SaveChanges();
    }

    public List<Client> ClientList() {
        return _dbContext.clients.ToList();
    }

    public bool CreateDb() {
        var isCreated = this._dbContext.Database.EnsureCreated();
        if (isCreated) {
            InitData();
            //var a =_dbContext.SaveChanges();
        }
        
        return isCreated;
    }

    public bool DeleteDb() {
        var isDeleted = this._dbContext.Database.EnsureDeleted();
        return isDeleted;
    }

    public Client FindAClient(int id) {
        return _dbContext.clients.Where(value => value.id == id).FirstOrDefault();
    }

    public List<Client> ShowAllClients() {
        return _dbContext.clients.ToList();
    }

    public bool AddClient(String name, String email, String phone, String address) {
        _dbContext.Add(
            new Client(name, email, phone, address)
        );
        var isAdded = _dbContext.SaveChanges();
        return true ? isAdded > 0 : false;
    }

    public bool DeleteClient(int Id){
        _dbContext.clients.Remove(_dbContext.clients.Where(value => value.id == Id).FirstOrDefault());
        var isDeleted = _dbContext.SaveChanges();
        return true ? isDeleted > 0 : false;
    }

    public bool EditClient(int Id, String name, String email, String phone, String address){
        var editClient = FindAClient(Id);
        editClient.name = name;
        editClient.email = email;
        editClient.phone = phone;
        editClient.address = address;
        var isUpdated = _dbContext.SaveChanges();
        return true ? isUpdated > 0 : false;
    }
}