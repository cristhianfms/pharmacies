import { Component, OnInit } from '@angular/core';
import {Pharmacy} from "../../../models/pharmacy.model";
import {PharmaciesService} from "../../../services/pharmacies.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-pharmacy-create',
  templateUrl: './pharmacy-create.component.html',
  styleUrls: ['./pharmacy-create.component.scss']
})
export class PharmacyCreateComponent implements OnInit {

  newPharmacy: Pharmacy = {
    name: "",
    address: ""
  }

  createStatus: 'loading' | 'success' | 'error' | null = null
  errorMessage: string = ''

  constructor(private pharmaciesServices: PharmaciesService, private router:Router) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.createStatus = 'loading'
    this.pharmaciesServices.create(this.newPharmacy).subscribe({
          next: this.handleUpdateResponse.bind(this),
          error: this.handleUpdateError.bind(this)
        }
    )
  }

  handleUpdateResponse(data: any){
    this.createStatus = 'success'
  }

  handleUpdateError(error: any){
    this.createStatus = 'error'
    this.errorMessage = error.error.message
  }

  goToPharmaciesHome() {
    this.router.navigate(['/admin/pharmacies']);
  }
}
