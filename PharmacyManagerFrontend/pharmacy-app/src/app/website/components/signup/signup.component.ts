import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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

  formSignup = new FormGroup({
    code: new FormControl(this.code, Validators.required),
    userName: new FormControl(this.invitation.userName, Validators.required),
    email: new FormControl(this.invitation.email, Validators.required),
    password: new FormControl(this.invitation.password, Validators.required),
  })

  Status: 'loading' | 'success' | 'error' | null = null
  errorMessage: string = ''

  constructor(private invitationService: InvitationsService, private router: Router) { }

  ngOnInit(): void {

  }
  submitSignup(){
    this.Status = 'loading';
    this.invitationService.update(this.code, this.invitation)
    .subscribe({
      next: this.handleOkResponse.bind(this),
      error: this.handleErrorResponse.bind(this)
    })
  }
  handleOkResponse(data:any){
    this.Status = 'success';
    this.router.navigate(['/home/login']);
  }
  handleErrorResponse(error: any){
    this.Status = 'error';
    this.errorMessage = error.error.message;
  }
}
