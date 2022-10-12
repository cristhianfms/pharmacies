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

        public SolicitudeLogic(ISolicitudeRepository solicitudeRepository,
            DrugLogic drugLogic,
            PharmacyLogic pharmacyLogic,
            Context currentContext)
        {
            this._solicitudeRepository = solicitudeRepository;
            this._drugLogic = drugLogic;
            this._pharmacyLogic = pharmacyLogic;
            this._context = currentContext;
        }

        public virtual Solicitude Create(Solicitude solicitude)
        {
            solicitude.PharmacyId = _context.CurrentUser.Pharmacy.Id;
            solicitude.Employee = _context.CurrentUser;

            try
            {
                foreach (SolicitudeItem itemToCheck in solicitude.Items)
                {
                    _pharmacyLogic.ExistsDrug(itemToCheck.DrugCode, solicitude.PharmacyId);
                }
            }
            catch (NullReferenceException)
            {
                throw new ValidationException("Items list cannot be null");
            }

            Solicitude createdSolicitude = _solicitudeRepository.Create(solicitude);

            return createdSolicitude;
        }

        public virtual void DrugExistsInSolicitude(Drug drug)
        {
            IEnumerable<Solicitude> solicitudes = _solicitudeRepository.GetAll();

            foreach (Solicitude s in solicitudes)
                if (s.PharmacyId == drug.PharmacyId)
                {
                    foreach (SolicitudeItem si in s.Items)
                        if (si.DrugCode == drug.DrugCode)
                        {
                            throw new ValidationException("This drug cannot be deleted" +
                        "because it's part of a solicitude");
                        }
                }
        }

        public IEnumerable<Solicitude> GetSolicitudes(QuerySolicitudeDto querySolicitudeDto)
        {
            IEnumerable<Solicitude> solicitudesToReturn = new List<Solicitude>();
            if (_context.CurrentUser.Role.Name.Equals(Role.EMPLOYEE))
            {
                solicitudesToReturn = _solicitudeRepository.GetAll(
                      s => s.Employee.Id == _context.CurrentUser.Id);


                if (querySolicitudeDto.State != null)
                {
                    State queryState = Enum.Parse<State>(querySolicitudeDto.State, true);
                    solicitudesToReturn = solicitudesToReturn.Where(s => s.State.Equals(queryState));
                }
                if (querySolicitudeDto.DrugCode != null)
                {
                    solicitudesToReturn = solicitudesToReturn.Where(s => s.Items.Any(x => x.DrugCode == querySolicitudeDto.DrugCode));
                }
                if (querySolicitudeDto.DateFrom != null && querySolicitudeDto.DateTo != null)
                {
                    DateTime dateFrom = toDateTime(querySolicitudeDto.DateFrom);
                    DateTime dateTo = toDateTime(querySolicitudeDto.DateTo);
                    validateDates(dateFrom, dateTo);
                    solicitudesToReturn = solicitudesToReturn.Where(s => s.Date >= dateFrom && s.Date <= dateTo);
                }
            }
            else if (_context.CurrentUser.Role.Name.Equals(Role.OWNER))
            {
                solicitudesToReturn = _solicitudeRepository.GetAll
                    (s => s.PharmacyId == _context.CurrentUser.OwnerPharmacyId);
            }

            return solicitudesToReturn;
        }
        private DateTime toDateTime(string stringDate)
        {
            return DateTime.Parse(stringDate);
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
