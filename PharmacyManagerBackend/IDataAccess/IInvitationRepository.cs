using Domain;

namespace IDataAccess;

public interface IInvitationRepository : IBaseRepository<Invitation>
{
    Invitation GetInvitationByCode(string code);
}
