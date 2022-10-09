import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SessionsService} from "../../services/sessions.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  token: string = ""
  constructor(private http: HttpClient, private sessionService: SessionsService) { }

  ngOnInit(): void {
  }

  login() {
    this.sessionService.login("Admin", "admin1234-")
        .subscribe(session => {
          console.log(session.token)
          this.token = session.token
        })
  }

  getProfile(){
    this.sessionService.profile()
        .subscribe(profile => {
          console.log(profile)
        })
  }
}
