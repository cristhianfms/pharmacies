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
            if (_context.CurrentUser.Role.Name.Equals("Employee"))
            {
                solicitudesToReturn = (List<Solicitude>)_solicitudeRepository.GetAll(
                     s => s.Employee.Id == _context.CurrentUser.Id);

                if (querySolicitudeDto.State != null)
                {
                  solicitudesToReturn = (List<Solicitude>)_solicitudeRepository.GetAll(
                    s => s.Employee.Id == _context.CurrentUser.Id &&
                    s.State.Equals(querySolicitudeDto.State));
                }
                if (querySolicitudeDto.DrugCode != null)
                {
                    solicitudesToReturn = (List<Solicitude>)_solicitudeRepository.GetAll(
                   s => s.Employee.Id == _context.CurrentUser.Id &&
                   s.Items.Any(x => x.DrugCode == querySolicitudeDto.DrugCode));
                }
                if(querySolicitudeDto.DateFrom != null && querySolicitudeDto.DateTo != null)
                {
                    DateTime dateFrom = toDateTime(querySolicitudeDto.DateFrom);
                    DateTime dateTo = toDateTime(querySolicitudeDto.DateTo);
                    validateDates(dateFrom, dateTo);
                    solicitudesToReturn = (List<Solicitude>)_solicitudeRepository.GetAll(
                    s => s.Employee.Id == _context.CurrentUser.Id &&
                    s.Date >= dateFrom &&
                    s.Date <= dateTo);
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
            throw new NotImplementedException();
        }
    }
}
