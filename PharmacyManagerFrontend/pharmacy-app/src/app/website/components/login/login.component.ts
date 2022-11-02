import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SessionsService} from "../../../services/sessions.service";
import {Router} from "@angular/router";
import { Credential } from 'src/app/models/credentials.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  credential: Credential = {
      userName:"",
      password:""
  };

  formLogin = new FormGroup({
    userName: new FormControl(this.credential.userName, Validators.required),
    password: new FormControl(this.credential.password, [Validators.required, Validators.minLength(7)])
  })

  Status: 'loading' | 'success' | 'error' | null = null
  errorMessage: string = ''

  constructor(private http: HttpClient, private sessionService: SessionsService, private router: Router) { }

  ngOnInit(): void {

  }

  submitLogin() {
    this.Status = 'loading';
    this.sessionService.loginAndGet(this.credential.userName, this.credential.password)
    .subscribe({  
      next: this.handleOkResponse.bind(this),
      error: this.handleErrorResponse.bind(this)
        })
}

  handleOkResponse(data:any){
    this.Status = 'success';
    var roleNameLowerCase = (data.roleName).toLowerCase();
    this.router.navigate(['/' + roleNameLowerCase]);
  }
  handleErrorResponse(error:any){
    this.Status = 'error';
    this.errorMessage = error.error.message;
  }

}
