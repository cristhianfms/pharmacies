import { Component, Input, OnInit } from '@angular/core';
import { Drug } from '../../../models/drug.model';
import { DrugsService } from '../../../services/drugs.service';
import { Router } from '@angular/router';
import {catchError, Observable, filter, of, take } from "rxjs";

@Component({
  selector: 'app-drug-list-delete',
  templateUrl: './drug-list-delete.component.html',
  styleUrls: ['./drug-list-delete.component.scss'],
})
export class DrugListDeleteComponent implements OnInit {
  drugs: Drug[]= [];

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
    this.drugService.getDrugs().pipe(
      take(1),
      catchError((err) => {
        console.log({err});
        return of(err);
      }),
    )
    .subscribe((drugs: Drug[]) => {
      this.setDrugs(drugs);
    });
  }

  deleteStatus: 'loading' | 'success' | 'error' | null = null;
  errorMessage: string = '';


  onDelete(drugId: number) {
    this.drugService.delete(drugId).subscribe({
      next: this.handleDeleteResponse.bind(this, drugId),
      error: this.handleDeleteError.bind(this),
    });
    this.drugService.delete(drugId).pipe(
      take(1),
      catchError((err) => {
        console.log({err});
        return of(err);
      }),
    ).subscribe((response) => {
      this.drugService.getAllDrugs()
      .pipe(
        take(1),
        catchError((err) => {
          console.log({err});
          return of(err);
        }),
      ).subscribe((drugs: Drug[] | undefined) => {
        this.setDrugs(drugs);
      });
    });
  }

  private setDrugs = (drugs: Drug[] | undefined) => {
    if(!drugs) this.drugs = [];
    else this.drugs = drugs;
  };


  handleDeleteResponse(data: any, drugId: number) {
    this.deleteStatus = 'success';
    const index = this.drugs.findIndex(d =>
      d.id === drugId);
    this.drugs.splice(index, 1);

  }

  handleDeleteError(error: any) {
    this.deleteStatus = 'error';
    this.errorMessage = error.error.message;
  }

}
