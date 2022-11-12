import { Pipe, PipeTransform } from '@angular/core';
import { QuerySolicitudeDto } from 'src/app/models/Dto/solicitude-query.model';
import { Solicitude, SolicitudeItem } from 'src/app/models/solicitude.model';

@Pipe({
  name: 'solicitudesFilter'
})
export class SolicitudesFilterPipe implements PipeTransform {

  transform(value: Solicitude[], querySolicitudeDto: QuerySolicitudeDto):  Solicitude[] {
    console.log(querySolicitudeDto.state);
    console.log(querySolicitudeDto.dateFrom);
    return value.filter(i => {
      return (querySolicitudeDto.dateFrom && querySolicitudeDto.dateTo) ? 
      ((i.date > querySolicitudeDto.dateFrom) &&(i.date < querySolicitudeDto.dateTo)): true  
      && querySolicitudeDto.state ? i.state == querySolicitudeDto.state : true &&
      querySolicitudeDto.drugCode ? i.solicitudeItems.filter(
        x => { x.drugCode == querySolicitudeDto.drugCode}) : true } )
      //querySolicitudeDto.dateTo ? i.date < querySolicitudeDto.dateTo : true &&
      //TODO: filter de drugCode
      //querySolicitudeDto.drugCode ? i.solicitudeItems.includes() 
  }
/*
searchDrug(items: SolicitudeItem[], drugCode : string){
  return items.filter(i=>{
    return i.drugCode == drugCode : true
  })
}*/

}
