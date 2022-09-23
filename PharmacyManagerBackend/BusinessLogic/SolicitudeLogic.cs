using Domain;
using Domain.Dtos;
using Exceptions;
using IBusinessLogic;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class SolicitudeLogic : ISolicitudeLogic
    {
        private readonly ISolicitudeRepository _solicitudeRepository;
        
        private Context _context;

        public SolicitudeLogic(ISolicitudeRepository solicitudeRepository)
        {
            this._solicitudeRepository = solicitudeRepository;
        }

        public void SetContext(User currentUser)
        {
             _context = new Context()
            {
                CurrentUser = currentUser
            };
        }

        public virtual Solicitude Create(Solicitude solicitude)
        {
           Solicitude createdSolicitude = _solicitudeRepository.Create(solicitude);
           
            return createdSolicitude;
        }

        public IEnumerable<Solicitude> GetSolicitudes(QuerySolicitudeDto querySolicitudeDto)
        {
            List<Solicitude> solicitudesToReturn = new List<Solicitude>();
            //List<Solicitude> solicitudes = new List<Solicitude>();
            if (_context.CurrentUser.Role.Name.Equals("Employee"))
            {
                //Ir aprovechando la lista y filtrando por cada cosa
               solicitudesToReturn = (List<Solicitude>)_solicitudeRepository.GetAll(
                     s => s.Employee.Id == _context.CurrentUser.Id);

                if (querySolicitudeDto.State != null)
                {
                    solicitudesToReturn.FindAll(s => s.State.Equals(querySolicitudeDto.State));
                  //solicitudesToReturn = (List<Solicitude>)_solicitudeRepository.GetAll(
                  //  s => s.Employee.Id == _context.CurrentUser.Id &&
                  //  s.State.Equals(querySolicitudeDto.State));
                }
                if (querySolicitudeDto.DrugCode != null)
                {
                    solicitudesToReturn.FindAll(s => s.Items.Any(x => x.DrugCode == querySolicitudeDto.DrugCode));
                   // solicitudesToReturn = (List<Solicitude>)_solicitudeRepository.GetAll(
                   //s => s.Employee.Id == _context.CurrentUser.Id &&
                   //s.Items.Any(x => x.DrugCode == querySolicitudeDto.DrugCode));
                }
                if(querySolicitudeDto.DateFrom != null && querySolicitudeDto.DateTo != null)
                {
                    DateTime dateFrom = toDateTime(querySolicitudeDto.DateFrom);
                    DateTime dateTo = toDateTime(querySolicitudeDto.DateTo);
                    validateDates(dateFrom, dateTo);
                    solicitudesToReturn.FindAll(s => s.Date >= dateFrom && s.Date <=dateTo);
                    //solicitudesToReturn = (List<Solicitude>)_solicitudeRepository.GetAll(
                    //s => s.Employee.Id == _context.CurrentUser.Id &&
                    //s.Date >= dateFrom &&
                    //s.Date <= dateTo);
                } 

            } 
            else if (_context.CurrentUser.Role.Name.Equals("Owner"))
            {
               solicitudesToReturn = (List<Solicitude>)_solicitudeRepository.GetAll
                   (s => s.Pharmacy.Id == _context.CurrentUser.Pharmacy.Id);
            }

            return solicitudesToReturn;
        }
        private DateTime toDateTime (string stringDate)
        {
          return  DateTime.Parse (stringDate);
        }
        private void validateDates(DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom > dateTo)
            {
                throw new ValidationException("The date from should be before date to");
            }
        }

        public Solicitude Update(int solicitudId, Solicitude solicitude)
        {
           return null;
            
            //Solicitude solicitudeToUpdate = _solicitudeRepository.GetFirst(solicitude.Id);

            /*
             Invitation invitation = getCreatedInvitation(invitationDto.Code);
        checkInvitationUserName(invitation, invitationDto.UserName);

        User userToCreate = new User()
        {
            UserName = invitation.UserName,
            Role = invitation.Role,
            Email = invitationDto.Email,
            Address = invitationDto.Address,
            Password = invitationDto.Password,
            RegistrationDate = DateTime.Now,
            // TODO dependiento del tipo
            //Pharmacy = invitation.Pharmacy
        };

        User createdUser = _userLogic.Create(userToCreate);
        _invitationRepository.Delete(invitation);

        InvitationDto invitationDtoToReturn = new InvitationDto()
        {
            UserName = userToCreate.UserName,
            UserId = createdUser.Id,
            RoleName = createdUser.Role.Name,
            PharmacyName = createdUser.Pharmacy?.Name,
            Email = createdUser.Email,
            Address = createdUser.Password,
        };

        return invitationDtoToReturn;
             */
        }
    }
}
