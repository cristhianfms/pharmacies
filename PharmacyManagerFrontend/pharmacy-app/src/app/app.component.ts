import {Component, OnInit} from '@angular/core';
import {TokenService} from "./services/token.service";
import {SessionsService} from "./services/sessions.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  constructor(private tokenService: TokenService, private sessionService: SessionsService) {
  }

  ngOnInit(): void {
    const token = this.tokenService.getToken();
    if (token) {
      this.sessionService.getProfile()
          .subscribe()
    }
  }
}
