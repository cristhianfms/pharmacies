using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class SolicitudeResponseModel
    {
        public int Id { get; set; }
        public string State { get; set; }
        public DateTime Date { get; set; }
        public List<SolicitudeItemModel> SolicitudeItems { get; set; }
    }
}
