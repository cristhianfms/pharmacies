import { Component, OnInit } from '@angular/core';
import {CreateInvitationDto, Invitation} from "../../../models/invitation.model";
import {Router} from "@angular/router";
import {InvitationsService} from "../../../services/invitations.service";
import {User} from "../../../models/user.model";
import {SessionsService} from "../../../services/sessions.service";

@Component({
  selector: 'app-invitation-create',
  templateUrl: './invitation-create.component.html',
  styleUrls: ['./invitation-create.component.scss']
})
export class InvitationCreateComponent implements OnInit {

  ownerProfile : User | null = null

  newInvitation : CreateInvitationDto = {
    userName: "",
    roleName: "",
    pharmacyName: ""
  }

  createdInvitation: Invitation | null = null;

  createStatus: 'loading' | 'success' | 'error' | null = null
  errorMessage: string = ''

  constructor(private router: Router, private invitationService: InvitationsService, private sessionService: SessionsService) { }

  ngOnInit(): void {
    this.sessionService.user$.subscribe(data => {
      this.ownerProfile = data;
    })
  }

  onSubmit() {
    this.createStatus = 'loading'
    this.invitationService.create(this.newInvitation).subscribe({
          next: this.handleUpdateResponse.bind(this),
          error: this.handleUpdateError.bind(this)
        }
    )
  }

  handleUpdateResponse(data: any){
    this.createdInvitation = data;
    this.createStatus = 'success'
  }

  handleUpdateError(error: any){
    this.createStatus = 'error'
    this.errorMessage = error.error.message
  }
}
