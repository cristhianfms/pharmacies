import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { environment } from "src/environments/environment";
import { CreateSolicitudeDto, Solicitude } from "../models/solicitude.model";

@Injectable({
    providedIn: 'root'
  })
  export class SolicitudesService {
  
    apiUrl: string = `${environment.API_URL}/api/solicitudes`
  
    selectedSolicitudeToEdit = new BehaviorSubject<Solicitude | null>(null)
    selectedSolicitudeToEdit$ = this.selectedSolicitudeToEdit.asObservable();
  
    constructor(private http: HttpClient) { }
  
    create(dto: CreateSolicitudeDto){
      return this.http.post(`${this.apiUrl}`, dto)
    }
  
    getAllSolicitudes() {
      return this.http.get<Solicitude[]>(`${this.apiUrl}`)
    }
  }
  