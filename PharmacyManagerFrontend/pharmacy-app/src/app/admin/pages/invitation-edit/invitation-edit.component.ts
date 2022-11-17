import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Invitation} from "../../../models/invitation.model";
import {InvitationsService} from "../../../services/invitations.service";

@Component({
    selector: 'app-invitation-edit',
    templateUrl: './invitation-edit.component.html',
    styleUrls: ['./invitation-edit.component.scss']
})
export class InvitationEditComponent implements OnInit {

    invitation: Invitation = {
        invitationCode: "",
        userName: "",
        roleName: "",
        pharmacyName: "",
        used: false
    };

    updatingStatus: 'loading' | 'success' | 'error' | null = null
    errorMessage: string = ''

    constructor(private router: Router, private route: ActivatedRoute, private invitationService: InvitationsService) {
    }

    ngOnInit(): void {
        this.invitationService.selectedInvitationToEdit$.subscribe((selectedInvitation) => {
                if (selectedInvitation){
                    this.invitation = selectedInvitation;
                }
            }
        )
    }


    goToInvitationsHome() {
        this.router.navigate(['/admin/invitations']);
    }

    onSubmit() {
        this.updatingStatus = 'loading'
        this.invitationService.updateAdmin(this.invitation.invitationCode, this.invitation).subscribe({
                next: this.handleUpdateResponse.bind(this),
                error: this.handleUpdateError.bind(this)
            }
        )
    }

    handleUpdateResponse(data: any){
        this.invitation = data;
        this.updatingStatus = 'success'
    }

    handleUpdateError(error: any){
        this.updatingStatus = 'error'
        this.errorMessage = error.error.message
    }
}
