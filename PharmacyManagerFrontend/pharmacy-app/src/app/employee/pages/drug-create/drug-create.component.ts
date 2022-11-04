import { Component, OnInit } from '@angular/core';
import {CreateDrugDTO, Drug} from "../../../models/drug.model";
import {Router} from "@angular/router";
import {DrugsService} from "../../../services/drugs.service";

@Component({
  selector: 'app-drug-create',
  templateUrl: './drug-create.component.html',
  styleUrls: ['./drug-create.component.scss']
})
export class DrugCreateComponent implements OnInit {

  newDrug : CreateDrugDTO = {
    drugCode: "",
    name: "",
    symptoms: "",
    quantityPerPresentation: 0,
    unitOfMeasurement:"",
    price: 0.0,
    needsPrescription: true,
    presentation:"",
    pharmacyId: 0,
    pharmacyName:""
  }

  constructor(
    private router: Router,
    private drugService: DrugsService) { }
  createdDrug: Drug | null = null;

  createStatus: 'loading' | 'success' | 'error' | null = null
  errorMessage: string = ''


  ngOnInit(): void {
  }

  goToDrugsHome() {
    this.router.navigate(['/drugs']);
  }

  onSubmit() {
    this.createStatus = 'loading'
    this.drugService.create(this.newDrug).subscribe({
          next: this.handleUpdateResponse.bind(this),
          error: this.handleUpdateError.bind(this)
        }
    )
  }

  handleUpdateResponse(data: any){
    this.createdDrug = data;
    this.createStatus = 'success'
    this.goToDrugsHome();
  }

  handleUpdateError(error: any){
    this.createStatus = 'error'
    this.errorMessage = error.error.message
  }
}
