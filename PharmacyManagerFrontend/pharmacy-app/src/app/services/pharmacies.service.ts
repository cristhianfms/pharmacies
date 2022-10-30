import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {CreateInvitationDto, Invitation} from "../models/invitation.model";
import {HttpClient} from "@angular/common/http";
import {Pharmacy} from "../models/pharmacy.model";

@Injectable({
  providedIn: 'root'
})
export class PharmaciesService {

  apiUrl: string = `${environment.API_URL}/api/pharmacies`

  constructor(private http: HttpClient) { }

  getAllPharmacies() {
    return this.http.get<Invitation[]>(`${this.apiUrl}`)
  }

    create(pharmacy: Pharmacy) {
      return this.http.post(`${this.apiUrl}`, pharmacy)
    }
}
