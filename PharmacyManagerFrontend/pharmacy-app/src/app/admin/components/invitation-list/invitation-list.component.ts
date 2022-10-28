import { Component, OnInit } from '@angular/core';
import {Invitation} from "../../../models/invitation.model";
import {InvitationsService} from "../../../services/invitations.service";

@Component({
  selector: 'app-invitation-list',
  templateUrl: './invitation-list.component.html',
  styleUrls: ['./invitation-list.component.scss']
})
export class InvitationListComponent implements OnInit {

  invitations: Invitation[] = []

  constructor(private invitationService: InvitationsService) { }

  ngOnInit(): void {
    this.invitationService.getAllInvitations().subscribe({
          next: this.handleGetAllResponse.bind(this),
          error: this.handleError.bind(this)
        }
    )
  }

  handleGetAllResponse(data: any){
    this.invitations = data
  }

  handleError(error: any){
    window.alert("Error getting invitations")
  }
}
