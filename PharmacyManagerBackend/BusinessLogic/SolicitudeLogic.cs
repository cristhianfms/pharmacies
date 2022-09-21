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
        private IBaseRepository<Solicitude> _solicitudeRepository;
        public SolicitudeLogic(IBaseRepository<Solicitude> solicitudeRepository)
        {
            this._solicitudeRepository = solicitudeRepository;
        }

        public virtual Solicitude Create(Solicitude solicitude)
        {
           Solicitude createdSolicitude = _solicitudeRepository.Create(solicitude);
           
            return createdSolicitude;
        }

        public List<Solicitude> GetSolicitudes(QuerySolicitudeDto querySolicitudeDto)
        {
            throw new NotImplementedException();
        }

        public Solicitude Update(int solicitudId, Solicitude solicitude)
        {
            throw new NotImplementedException();
        }
    }
}
