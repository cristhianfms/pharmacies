import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {PurchaseDto} from "../models/Dto/purchase-dto.model";

@Injectable({
  providedIn: 'root'
})
export class PurchasesService {

  apiUrl: string = `${environment.API_URL}/api/purchases`

  constructor( private http: HttpClient) { }

  create(purchase: PurchaseDto){
    return this.http.post(`${this.apiUrl}`, purchase)
  }

}
