import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {switchMap} from "rxjs";
import {Invitation} from "../../models/invitation.model";
import {InvitationsService} from "../../services/invitations.service";

@Component({
    selector: 'app-invitation-edit',
    templateUrl: './invitation-edit.component.html',
    styleUrls: ['./invitation-edit.component.scss']
})
export class InvitationEditComponent implements OnInit {

    invitationId: number | null = null
    invitation: Invitation | null = null

    constructor(private route: ActivatedRoute, private invitationService: InvitationsService) {
    }

    ngOnInit(): void {
        this.route.paramMap
            .pipe(
                switchMap(params => {
                        const id = params.get('id');
                        if (id) {
                            this.invitationId = Number(params.get('id'))
                            return this.invitationService.getInvitation(this.invitationId);
                        }
                        return [null]
                    }
                )).subscribe(
            data => this.invitation = data
        )
    }
}
