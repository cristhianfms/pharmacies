import { Component, OnInit } from '@angular/core';
import {Drug} from "../../../models/drug.model";
import {Router} from "@angular/router";
import {DrugsService} from "../../../services/drugs.service";

@Component({
  selector: 'app-drug-delete',
  templateUrl: './drug-delete.component.html',
  styleUrls: ['./drug-delete.component.scss']
})
export class DrugDeleteComponent implements OnInit {

  drug: Drug = {
    id: 0,
    name: "",
    drugCode: "",
    symptoms: "",
    quantityPerPresentation: 0,
    unitOfMeasurement:"",
    price: 0.0,
    needsPrescription: true,
    presentation:"",
    stock: 0,
    pharmacyId:0,
    pharmacyName:""
  }

  constructor(
    private router: Router,
    private drugService: DrugsService) { }

  deleteStatus: 'loading' | 'success' | 'error' | null = null
  errorMessage: string = ''


  ngOnInit(): void {
    this.drugService.selectedDrugToDelete$.subscribe((selectedDrug) => {
            if (selectedDrug){
                this.drug = selectedDrug;
            }
        }
    )
}

  goTodrugsHome() {
    this.router.navigate(['/admin/drugs']);
  }

  onSubmit() {
    this.deleteStatus = 'loading'
    this.drugService.delete(this.drug.id).subscribe({
          next: this.handleUpdateResponse.bind(this),
          error: this.handleUpdateError.bind(this)
        }
    )
  }

  handleUpdateResponse(data: any){
    this.deleteStatus = 'success'
  }

  handleUpdateError(error: any){
    this.deleteStatus = 'error'
    this.errorMessage = error.error.message
  }
}
