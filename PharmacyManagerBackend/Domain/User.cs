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

    public Pharmacy? Pharmacy
    {
        get
        {
            return OwnerPharmacy != null ? OwnerPharmacy : EmployeePharmacy;
        }
        set
        {
            if (Role.OWNER.Equals(Role.Name))
            {
                OwnerPharmacy = value;
            }
            else if (Role.EMPLOYEE.Equals(Role.Name))
            {
                EmployeePharmacy = value;
            }
        }
    }

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