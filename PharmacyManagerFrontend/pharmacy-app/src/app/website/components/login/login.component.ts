import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SessionsService} from "../../../services/sessions.service";
import {Router} from "@angular/router";
import { Credential } from 'src/app/models/credentials.model';
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
  constructor(private http: HttpClient, private sessionService: SessionsService, private router: Router) { }

  ngOnInit(): void {

  }

  onSubmit() {
    console.log(this.credential)
    this.sessionService.login(this.credential.userName, this.credential.password)
    .subscribe(session =>{
          console.log(session.token);   
          alert(session.token);   
          this.router.navigate(['/home']);
          //next:  this.router.navigate(['/home']),
          //error: this.router.navigate(['**'])
        })
        //this.sessionService.
}
submitLogin() {
  console.log(this.credential)
  this.sessionService.loginAndGet(this.credential.userName, this.credential.password)
  .subscribe(session =>{  
        alert(session.roleName);   
        this.router.navigate(['/' + session.roleName]);
        //next:  this.router.navigate(['/home']),
        //error: this.router.navigate(['**'])
      })
}
}
