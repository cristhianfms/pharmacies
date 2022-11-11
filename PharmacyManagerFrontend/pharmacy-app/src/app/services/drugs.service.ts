import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpParams, HttpStatusCode} from "@angular/common/http";
import {CreateDrugDTO, Drug} from "../models/drug.model";
import {environment} from "../../environments/environment";
import {BehaviorSubject, catchError, throwError, tap, Observable} from "rxjs";
import { DrugQueryDto } from '../models/Dto/drug-query.model';

@Injectable({
  providedIn: 'root'
})
export class DrugsService {

  apiUrl: string = `${environment.API_URL}/api/drugs`

  private _drugsBehaviorSubject$: BehaviorSubject<Drug[] | undefined>;

  constructor( private http: HttpClient) {
    this._drugsBehaviorSubject$ = new BehaviorSubject<Drug[] | undefined>(undefined);
  }

   private selectedDrugToDelete = new BehaviorSubject<Drug[]>([])
  selectedDrugToDelete$ = this.selectedDrugToDelete.asObservable();

  getAllDrugs(pharmacyName?: string, drugCode?:string) {
    let params = new HttpParams()
    if(pharmacyName) {
      params.set('pharmacyName', pharmacyName)
    }
    if(drugCode) {
      params.set('drugCode', drugCode)
    }

    return this.http.get<Drug[]>(`${this.apiUrl}`, {params})
        .pipe(
            catchError((error: HttpErrorResponse) => {
              if (error.status === HttpStatusCode.InternalServerError) {
                return throwError(() => new Error("Issue with the server"));
              }
              return throwError(() => new Error("Something went wrong"))
            })
        )
  }

  getDrugs(): Observable<Drug[]> {
    let params = new HttpParams()
    return this.http.get<Drug[]>(`${this.apiUrl}`, {params}).pipe(
      tap((drugs: Drug[]) => this._drugsBehaviorSubject$.next(drugs)),
    );
  }

  create(dto: CreateDrugDTO){
    return this.http.post(`${this.apiUrl}`, dto)
  }

  getDrug(id: number){
    return this.http.get<Drug>(`${this.apiUrl}/${id}`)
  }

  delete(id: number){
    return this.http.delete<any>(`${this.apiUrl}/${id}`)
  }
}
