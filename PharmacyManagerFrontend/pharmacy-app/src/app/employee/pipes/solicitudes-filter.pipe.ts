import { Pipe, PipeTransform } from '@angular/core';
import { QuerySolicitudeDto } from 'src/app/models/Dto/solicitude-query.model';
import { Solicitude } from 'src/app/models/solicitude.model';

@Pipe({
  name: 'solicitudesFilter'
})
export class SolicitudesFilterPipe implements PipeTransform {

  transform(value: Solicitude[], querySolicitudeDto: QuerySolicitudeDto):  Solicitude[] {
    console.log(querySolicitudeDto.state);
    console.log(querySolicitudeDto.dateFrom);
    return value.filter(i => {
      return querySolicitudeDto.dateFrom && querySolicitudeDto.dateTo ? 
      (i.date > querySolicitudeDto.dateFrom) && querySolicitudeDto.dateTo ?
      (i.date < querySolicitudeDto.dateTo) : true : true &&
      //querySolicitudeDto.dateTo ? i.date < querySolicitudeDto.dateTo : true &&
      //TODO: filter de drugCode
      //querySolicitudeDto.drugCode ? i.solicitudeItems.includes() 
      querySolicitudeDto.state ? i.state == querySolicitudeDto.state : true
    })
  }


}
