import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SessionsService} from "../../../services/sessions.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private http: HttpClient, private sessionService: SessionsService, private router: Router) { }

  ngOnInit(): void {
  }

  login() {
    this.sessionService.loginAndGet("Admin", "admin1234-")
        .subscribe(data => {
            console.log(data)
            this.router.navigate(['/admin']);
        })
  }
}
