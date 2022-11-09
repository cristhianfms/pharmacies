import { Pipe, PipeTransform } from '@angular/core';
import {Drug} from "../../../../models/drug.model"
import {DrugQueryDto} from "../../../../models/Dto/drug-query.model";

@Pipe({
  name: 'drugsFilter'
})
export class DrugsFilterPipe implements PipeTransform {

  transform(value: Drug[], drugQueryDto: DrugQueryDto): Drug[] {
    return value.filter(d => {
      return drugQueryDto.drugName ? d.name == drugQueryDto.drugName : true &&
              drugQueryDto.hasStock ? d.stock > 0 : true
    })
  }

}
