import { Pipe, PipeTransform } from '@angular/core';
import { QuerySolicitudeDto } from 'src/app/models/Dto/solicitude-query.model';
import { Solicitude, SolicitudeItem } from 'src/app/models/solicitude.model';

@Pipe({
  name: 'solicitudesFilter'
})
export class SolicitudesFilterPipe implements PipeTransform {

  transform(value: Solicitude[], querySolicitudeDto: QuerySolicitudeDto):  Solicitude[] {
    console.log(querySolicitudeDto)
    return value.filter(i => {
      return (querySolicitudeDto?.dateFrom ? i.date >= querySolicitudeDto.dateFrom : true) &&
            (querySolicitudeDto?.dateTo ? i.date <= querySolicitudeDto.dateTo : true) &&
            (querySolicitudeDto?.state ? i.state == querySolicitudeDto.state : true) &&
            (querySolicitudeDto?.drugCode ? i.solicitudeItems.findIndex(x => x.drugCode == querySolicitudeDto.drugCode) > -1 : true)
    })};
}
