import { Component, Input, OnInit } from '@angular/core';
import { Drug } from '../../../models/drug.model';
import { DrugsService } from '../../../services/drugs.service';
import {DrugQueryDto} from "../../../models/Dto/drug-query.model";
import { Router } from '@angular/router';

@Component({
  selector: 'app-drug-list',
  templateUrl: './drug-list.component.html',
  styleUrls: ['./drug-list.component.scss'],
})
export class DrugListComponent implements OnInit {
  drugs: Drug[] = [];

  drug: Drug = {
    id: 0,
    name: '',
    drugCode: '',
    symptoms: '',
    quantityPerPresentation: 0,
    unitOfMeasurement: '',
    price: 0.0,
    needsPrescription: true,
    presentation: '',
    stock: 0,
    pharmacyId: 0,
    pharmacyName: '',
  };

  /*@Input() DrugQuery: DrugQueryDto = {
    pharmacyName: null,
    userName: null,
    role: null
  }*/

  constructor(
    private drugService: DrugsService, private router: Router
  )
  {}

  ngOnInit(): void {
    this.drugService.getAllDrugs().subscribe({
      next: this.handleGetAllResponse.bind(this),
      error: this.handleError.bind(this),
    }),
      this.drugService.selectedDrugToDelete$.subscribe((selectedDrug) => {
        if (selectedDrug) {
          this.drug = selectedDrug;
        }
      });
  }

  deleteStatus: 'loading' | 'success' | 'error' | null = null;
  errorMessage: string = '';

  handleGetAllResponse(data: any) {
    this.drugs = data;
  }

  refresh():void{
    this.router.navigate(['/employee/drugs']);
  }

  handleError(error: any) {
    window.alert('Error getting Drugs');
  }

  onDelete(drugId: number) {
    this.drugService.delete(drugId).subscribe({
      next: this.handleDeleteResponse.bind(this),
      error: this.handleDeleteError.bind(this),
    });
  }

  handleDeleteResponse(data: any) {
    this.deleteStatus = 'success';
    this.refresh();
  }

  handleDeleteError(error: any) {
    this.deleteStatus = 'error';
    this.errorMessage = error.error.message;
  }
}
