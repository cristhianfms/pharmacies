import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Invitation} from "../../../models/invitation.model";
import {InvitationsService} from "../../../services/invitations.service";
import {NgForm} from "@angular/forms";

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

    userName = ""

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

    onSubmit(f: NgForm) {

    }
}
