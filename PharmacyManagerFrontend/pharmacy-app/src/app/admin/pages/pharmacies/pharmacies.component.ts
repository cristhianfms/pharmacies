import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-pharmacies',
  templateUrl: './pharmacies.component.html',
  styleUrls: ['./pharmacies.component.scss']
})
export class PharmaciesComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  onCreatePharmacy() {
    this.router.navigate(['/admin/pharmacy-create']);
  }
}
