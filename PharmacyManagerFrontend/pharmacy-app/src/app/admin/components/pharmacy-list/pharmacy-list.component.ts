import { Component, OnInit } from '@angular/core';
import {Pharmacy} from "../../../models/pharmacy.model";
import {Router} from "@angular/router";
import {PharmaciesService} from "../../../services/pharmacies.service";

@Component({
  selector: 'app-pharmacy-list',
  templateUrl: './pharmacy-list.component.html',
  styleUrls: ['./pharmacy-list.component.scss']
})
export class PharmacyListComponent implements OnInit {

  pharmacies : Pharmacy[] = []

  constructor(private pharmacyService: PharmaciesService, private router: Router) { }

  ngOnInit(): void {
    this.pharmacyService.getAllPharmacies().subscribe({
          next: this.handleGetAllResponse.bind(this),
          error: this.handleError.bind(this)
        }
    )
  }

  handleGetAllResponse(data: any){
    this.pharmacies = data
  }

  handleError(error: any){
    window.alert("Error getting pharmacies")
  }
}
