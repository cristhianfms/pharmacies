import { Pipe, PipeTransform } from '@angular/core';
import { SolicitudeQueryDto } from 'src/app/models/Dto/solicitude-query.model';
import { Solicitude } from 'src/app/models/solicitude.model';

@Pipe({
  name: 'solicitudesFilter'
})
export class SolicitudesFilterPipe implements PipeTransform {

  transform(value: Solicitude[], solicitudeQueryDto: SolicitudeQueryDto):  Solicitude[] {
    console.log(solicitudeQueryDto.state);
    console.log(solicitudeQueryDto.dateFrom);
    return value.filter(i => {
      return solicitudeQueryDto.dateFrom ? i.date >= solicitudeQueryDto.dateFrom : true &&
      solicitudeQueryDto.dateTo ? i.date <= solicitudeQueryDto.dateTo : true &&
      //TODO: filter de drugCode
      //solicitudeQueryDto.drugCode ? i.solicitudeItems.includes() 
      solicitudeQueryDto.state ? i.state == solicitudeQueryDto.state : true
    })
  }

}
