namespace Note.API.Models.Entities;

public class UserModel {
    public Guid id {get;set;}
    public String? Username {get;set;}
    public String? Password {get;set;}
    public String? FirstName {get;set;}
    public String? LastName {get;set;}
    public String? Email {get;set;}

    // User or admin
    public required String Role {get;set;}
}