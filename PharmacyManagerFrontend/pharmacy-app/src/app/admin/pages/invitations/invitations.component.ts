import { Component, OnInit } from '@angular/core';
import {InvitationQueryDto} from "../../../models/Dto/invitation-query.model";
import {Router} from "@angular/router";

@Component({
  selector: 'app-invitations',
  templateUrl: './invitations.component.html',
  styleUrls: ['./invitations.component.scss']
})
export class InvitationsComponent implements OnInit {

  filterPharmacyName: string | null = null
  filterUserName: string | null = null
  filterRole: string | null = null

  invitationQuery: InvitationQueryDto = {
    pharmacyName: null,
    userName: null,
    role: null
  }

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  updateInvitationQuery() {
    let role = this.filterRole == "All" ? null : this.filterRole;
    this.invitationQuery = {
      pharmacyName: this.filterPharmacyName,
      userName: this.filterUserName,
      role: role
    }
  }

    onCreateInvitation() {
      this.router.navigate(['/admin/invitation-create']);
    }
}
