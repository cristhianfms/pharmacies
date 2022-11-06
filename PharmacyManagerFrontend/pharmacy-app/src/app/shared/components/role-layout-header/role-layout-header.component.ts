import { Component, OnInit } from '@angular/core';
import {User} from "../../../models/user.model";
import {SessionsService} from "../../../services/sessions.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-role-layout-header',
  templateUrl: './role-layout-header.component.html',
  styleUrls: ['./role-layout-header.component.scss']
})
export class RoleLayoutHeaderComponent implements OnInit {

  userProfile : User | null = null

  constructor(private sessionService: SessionsService, private router: Router) { }

  ngOnInit(): void {
      this.sessionService.user$.subscribe(data => {
        this.userProfile = data;
        console.log(this.userProfile)
    })
  }

  onLogout() {
    this.sessionService.logout();
    this.router.navigate(['/']);
  }
}
