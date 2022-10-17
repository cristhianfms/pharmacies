using IDataAccess;
using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class LogicHandler
    {
        private DrugLogic _drugLogic;
                    
        public LogicHandler(DrugLogic drugLogic)
        {
            _drugLogic = drugLogic;
        }

        public void AddStock(IEnumerable<SolicitudeItem> solicitudes)
        {
            _drugLogic.AddStock(solicitudes);
        }

        public void Update(int id, Drug drug)
        {
            _drugLogic.Update(id, drug);
        }
    }
}
