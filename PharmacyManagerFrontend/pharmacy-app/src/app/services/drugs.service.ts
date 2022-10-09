import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpParams, HttpStatusCode} from "@angular/common/http";
import {CreateDrugDTO, Drug} from "../models/drug.model";
import {environment} from "../../environments/environment";
import {catchError, throwError} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class DrugsService {

  apiUrl: string = `${environment.API_URL}/api/drugs`

  constructor( private http: HttpClient) { }

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
