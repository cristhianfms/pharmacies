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
        private readonly DrugLogic _drugLogic;
        private readonly PharmacyLogic _pharmacyLogic;
        private Context _context;

        public SolicitudeLogic(ISolicitudeRepository solicitudeRepository, DrugLogic drugLogic, PharmacyLogic pharmacyLogic)
        {
            this._solicitudeRepository = solicitudeRepository;
            this._drugLogic = drugLogic;
            this._pharmacyLogic = pharmacyLogic;
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
            solicitude.Employee = _context.CurrentUser;
            solicitude.PharmacyId = _context.CurrentUser.Pharmacy.Id;
            solicitude.Pharmacy = _context.CurrentUser.Pharmacy;
           Solicitude createdSolicitude = _solicitudeRepository.Create(solicitude);
           
            return createdSolicitude;
        }

        public IEnumerable<Solicitude> GetSolicitudes(QuerySolicitudeDto querySolicitudeDto)
        {
            List<Solicitude> solicitudesToReturn = new List<Solicitude>();
            if (_context.CurrentUser.Role.Name.Equals(Role.EMPLOYEE))
            {
               solicitudesToReturn = (List<Solicitude>)_solicitudeRepository.GetAll(
                     s => s.Employee.Id == _context.CurrentUser.Id);
                if (querySolicitudeDto.State != null)
                {
                    solicitudesToReturn.FindAll(s => s.State.Equals(querySolicitudeDto.State));
                }
                if (querySolicitudeDto.DrugCode != null)
                {
                    solicitudesToReturn.FindAll(s => s.Items.Any(x => x.DrugCode == querySolicitudeDto.DrugCode));
                }
                if(querySolicitudeDto.DateFrom != null && querySolicitudeDto.DateTo != null)
                {
                    DateTime dateFrom = toDateTime(querySolicitudeDto.DateFrom);
                    DateTime dateTo = toDateTime(querySolicitudeDto.DateTo);
                    validateDates(dateFrom, dateTo);
                    solicitudesToReturn.FindAll(s => s.Date >= dateFrom && s.Date <=dateTo);
                } 
            } 
            else if (_context.CurrentUser.Role.Name.Equals(Role.OWNER))
            {
               solicitudesToReturn = (List<Solicitude>)_solicitudeRepository.GetAll
                   (s => s.PharmacyId == _context.CurrentUser.OwnerPharmacyId);
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

        public Solicitude Update(int solicitudeId, Solicitude newSolicitude)
        {
            Solicitude solicitudeToUpdate = getSolicitude(solicitudeId);

            if (solicitudeToUpdate.State.Equals(State.PENDING))
            {
                if (newSolicitude.State.Equals(State.ACCEPTED))
                {

                    _drugLogic.AddStock(solicitudeToUpdate.Items);
                }

                solicitudeToUpdate.State = newSolicitude.State;
                _solicitudeRepository.Update(solicitudeToUpdate);
            }

            return solicitudeToUpdate;


        }

        private Solicitude getSolicitude(int solicitudeId)
        {
            Solicitude solicitude;
            try
            {
                solicitude = _solicitudeRepository.GetFirst(s => s.Id == solicitudeId);
            }
            catch (ResourceNotFoundException e)
            {
                throw new ResourceNotFoundException("solicitude doesn't exist");
            }

            return solicitude;
        }

    }
}
