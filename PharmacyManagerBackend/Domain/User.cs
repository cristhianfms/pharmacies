namespace Domain;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Password { get; set; }
    public int? OwnerPharmacyId { get; set; }
    public Pharmacy? OwnerPharmacy { get; set; }
    public int? EmployeePharmacyId { get; set; }
    public Pharmacy? EmployeePharmacy { get; set; }
    public DateTime RegistrationDate { get; set; }
    //TODO: usar pharmacy para setear la employee pharmacy o owner pharmacy
    public Pharmacy? Pharmacy {get; set;}

    public override bool Equals(object obj)
    {
        return obj is User user &&
                Id == user.Id &&
                UserName == user.UserName &&
                RoleId == user.RoleId &&
                Email == user.Email &&
                Address == user.Address &&
                Password == user.Password &&
                OwnerPharmacyId == user.OwnerPharmacyId &&
                EmployeePharmacyId == user.EmployeePharmacyId &&
                RegistrationDate == user.RegistrationDate;
    }
}