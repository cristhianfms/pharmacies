import { Component, Input, OnInit } from '@angular/core';
import { Drug } from '../../../models/drug.model';
import { DrugsService } from '../../../services/drugs.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-drug-list-delete',
  templateUrl: './drug-list-delete.component.html',
  styleUrls: ['./drug-list-delete.component.scss'],
})
export class DrugListDeleteComponent implements OnInit {
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

  /*removeDrug(drugToDelete: Drug) {
    const index = this.drugs.findIndex(d =>
        d.drugCode === drugToDelete.drugCode);
    this.drugs.splice(index, 1);

    this.myCart.next(this.myShoppingCart);
  }*/

  deleteStatus: 'loading' | 'success' | 'error' | null = null;
  errorMessage: string = '';

  handleGetAllResponse(data: any) {
    this.drugs = data;
  }

  refresh():void{
     this.drugs;
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
