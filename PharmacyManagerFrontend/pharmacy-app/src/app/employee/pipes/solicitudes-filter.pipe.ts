import { Pipe, PipeTransform } from '@angular/core';
import { QuerySolicitudeDto } from 'src/app/models/Dto/solicitude-query.model';
import { Solicitude, SolicitudeItem } from 'src/app/models/solicitude.model';

@Pipe({
  name: 'solicitudesFilter'
})
export class SolicitudesFilterPipe implements PipeTransform {

  transform(value: Solicitude[], querySolicitudeDto: QuerySolicitudeDto):  Solicitude[] {
    console.log(querySolicitudeDto)
    return value.filter((i: Solicitude) => {
      let dateFrom = null
      let dateTo = null
      if(querySolicitudeDto?.dateFrom && querySolicitudeDto?.dateTo) {
        dateFrom = new Date(querySolicitudeDto.dateFrom)
        dateFrom.setUTCHours(0,0,0,0);
        dateTo = new Date(querySolicitudeDto.dateTo)
        dateTo.setUTCHours(23,59,59,999);
      }
      return (dateFrom !== null ? new Date(i.date) >= dateFrom : true) &&
            (dateTo != null ? new Date(i.date) <= dateTo : true) &&
            (querySolicitudeDto?.state ? i.state == querySolicitudeDto.state : true) &&
            (querySolicitudeDto?.drugCode ? i.solicitudeItems.findIndex(x => x.drugCode == querySolicitudeDto.drugCode) > -1 : true)
    })};
}
