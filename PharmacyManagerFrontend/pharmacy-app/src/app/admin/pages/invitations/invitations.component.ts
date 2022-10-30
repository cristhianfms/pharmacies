import { Component, OnInit } from '@angular/core';
import {InvitationQueryDto} from "../../../models/Dto/invitation-query.model";

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

  constructor() { }

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
}
