import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {CreateDrugDTO, Drug} from "../models/drug.model";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class DrugsService {

  apiUrl: string = `${environment}/api/drugs`

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
