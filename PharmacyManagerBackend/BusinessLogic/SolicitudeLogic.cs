using Domain;
using Domain.Dtos;
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
            List <Solicitude> solicitudesToReturn = new List<Solicitude>();
            if (_context.CurrentUser.Role.Name.Equals("Employee"))
            {
               solicitudesToReturn = (List<Solicitude>)_solicitudeRepository.GetAll
                    (s => s.Employee.Id == _context.CurrentUser.Id);
            } else if (_context.CurrentUser.Role.Name.Equals("Owner"))
            {
               solicitudesToReturn = (List<Solicitude>)_solicitudeRepository.GetAll
                   (s => s.Pharmacy.Id == _context.CurrentUser.Pharmacy.Id);
            }


            return solicitudesToReturn;
        }

        public Solicitude Update(int solicitudId, Solicitude solicitude)
        {
            throw new NotImplementedException();
        }
    }
}
