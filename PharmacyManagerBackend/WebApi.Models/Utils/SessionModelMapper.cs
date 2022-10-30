using Domain.AuthDomain;

namespace WebApi.Models.Utils;

public class SessionModelMapper
{
    public static SessionProfileModel ToModel(Session session)
    {
        return new SessionProfileModel()
        {
            RoleName = session.User.Role.Name,
            UserName = session.User.UserName,
            PharmacyName = session.User.Pharmacy?.Name
        };
    }
}