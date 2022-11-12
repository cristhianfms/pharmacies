import {Component, Input, OnInit} from '@angular/core';
import {Invitation} from "../../../models/invitation.model";
import {InvitationsService} from "../../../services/invitations.service";
import {InvitationQueryDto} from "../../../models/Dto/invitation-query.model";
import {Router} from "@angular/router";

@Component({
  selector: 'app-invitation-list',
  templateUrl: './invitation-list.component.html',
  styleUrls: ['./invitation-list.component.scss']
})
export class InvitationListComponent implements OnInit {

  invitations: Invitation[] = []

  @Input() invitationQuery: InvitationQueryDto = {
    pharmacyName: null,
    userName: null,
    role: null
  }

  constructor(private invitationService: InvitationsService, private router: Router) { }

  ngOnInit(): void {
    this.invitationService.getAllInvitations().subscribe({
          next: this.handleGetAllResponse.bind(this)
        }
    )
  }

  handleGetAllResponse(data: any){
    this.invitations = data
  }

  onEdit(invitation: Invitation) {
    this.invitationService.selectedInvitationToEdit.next(invitation);
    this.router.navigate(['/admin/invitation-edit/' + invitation.invitationCode]);
  }

  isDisable(invitation: Invitation) {
    return invitation.used
  }
}
