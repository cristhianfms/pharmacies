import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Invitation, UpdateInvitationDto } from 'src/app/models/invitation.model';
import { InvitationsService } from 'src/app/services/invitations.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {
  code: string = "";
  invitation: UpdateInvitationDto = {
    userName: "",
    roleName: "",
    pharmacyName: "",
    email: "",
    address: "",
    password:"",
  };
  constructor(private invitationService: InvitationsService, private router: Router) { }

  ngOnInit(): void {

  }
  submitSignup(){
    this.invitationService.update(this.code, this.invitation)
    .subscribe(data =>{
      console.log(data.invitationCode);
      alert(data.userName);
      this.router.navigate(['/home/login']);
    } )
  }
}
