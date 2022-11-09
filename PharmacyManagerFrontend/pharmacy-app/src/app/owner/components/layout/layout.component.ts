import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SessionsService } from 'src/app/services/sessions.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  constructor(private router: Router, private sessionService: SessionsService) { }

  ngOnInit(): void {
  }
  logOut(){
    this.sessionService.logout();
    this.router.navigate([""]);
}
}
