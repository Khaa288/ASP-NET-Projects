using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("Client")]
public class Client {
    [Key]
    public int id {set; get;}

    [StringLength(100)]
    [Required]
    public String name {set; get;}

    [StringLength(150)]
    [Required]
    public String email {set; get;}

    [StringLength(20)]
    public String phone {set; get;}

    [StringLength(100)]
    public String address {set; get;}

    [Required]
    public String createAt {set; get;}

    // public Client(){}
    public Client(String name, String email, String phone, String address) {
        this.name = name;
        this.email = email;
        this.phone = phone;
        this.address = address;
        this.createAt = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second} ";
    }
}