import { Component, OnInit } from '@angular/core';
import {InvitationsService} from "../../services/invitations.service";
import {Invitation, UpdateInvitationDto} from "../../models/invitation.model";

@Component({
  selector: 'app-invitations',
  templateUrl: './invitations.component.html',
  styleUrls: ['./invitations.component.scss']
})
export class InvitationsComponent implements OnInit {

  invitations: Invitation [] = []

  invitationChosen: Invitation = {
    id: 0,
    userName: '',
    pharmacyName: '',
    roleName: ''
  }
  constructor(private invitationService: InvitationsService) { }

  ngOnInit(): void {
  }

  updateInvitation(){
    const changes: UpdateInvitationDto = {
      pharmacyName: 'nuevaFarmacia'
    }
    const id = this.invitationChosen.id
    this.invitationService.update(id, changes).subscribe( data => {
      const invitationIndex = this.invitations.findIndex(i => i.id === this.invitationChosen.id);
      this.invitations[invitationIndex] = data;
    })
  }
}
