import { Pipe, PipeTransform } from '@angular/core';
import {Invitation} from "../../models/invitation.model";
import {InvitationQueryDto} from "../../models/Dto/invitation-query.model";

@Pipe({
  name: 'invitationsFilter'
})
export class InvitationsFilterPipe implements PipeTransform {

  transform(value: Invitation[], invitationQueryDto: InvitationQueryDto):  Invitation[] {
    return value.filter(i => {
      return invitationQueryDto.userName ? i.userName == invitationQueryDto.userName : true &&
              invitationQueryDto.pharmacyName ? i.pharmacyName == invitationQueryDto.pharmacyName : true &&
              invitationQueryDto.role ? i.roleName == invitationQueryDto.role : true
    })
  }

}
