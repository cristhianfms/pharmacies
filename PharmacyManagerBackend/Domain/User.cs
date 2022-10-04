using Exceptions;

namespace Domain;

public class User
{
    private const int PASSWORD_MIN_LENGTH = 8;
    private string _email;
    private string _password;
    public int Id { get; set; }
    public string UserName { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }

    public string Email
    {
        get
        {
            return _email;
        }
        set
        {
            checkValidEmail(value);
            _email = value;
        }
    }
    public string Address { get; set; }

    public string Password
    {
        get
        {
            return _password;
        }
        set
        {
            checkValidPassworkd(value);
            _password = value;
        }
    }
    
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
    
    private void checkValidEmail(string email)
    {
        if (String.IsNullOrEmpty(email))
        {
            throw new ValidationException("email can't be empty");
        } else if (!isValidEmailFormat(email))
        {
            throw new ValidationException("email bad format");
        }
    }
    private bool isValidEmailFormat(string email)
    {
        // reference: https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith(".")) {
            return false;
        }
        try {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch {
            return false;
        }
    }
    
    private void checkValidPassworkd(string password)
    {
        if (String.IsNullOrEmpty(password))
        {
            throw new ValidationException("password can't be empty");
        } else if (password.Length < PASSWORD_MIN_LENGTH)
        {
            throw new ValidationException("passowrd must contain at least 8 characters");
        } else if (!hasSpecialChar(password))
        {
            throw new ValidationException("password must contain at least one special character");
        }
    }

    private bool hasSpecialChar(string password)
    {
        //reference: https://stackoverflow.com/questions/4503542/check-for-special-characters-in-a-string
        return password.Any(ch => !Char.IsLetterOrDigit(ch));
    }
}