import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import {BehaviorSubject, map} from "rxjs";
import { environment } from "src/environments/environment";
import {CreateSolicitudeDto, Solicitude} from "../models/solicitude.model";
import {SolicitudeGetDto, SolicitudePutDto} from "../models/Dto/solicitude-dto.model";
import {SolicitudeState} from "../models/Dto/solicitude-state-dto.model";

@Injectable({
    providedIn: 'root'
  })
  export class SolicitudesService {

    apiUrl: string = `${environment.API_URL}/api/solicitudes`

    selectedSolicitudeToEdit = new BehaviorSubject<Solicitude | null>(null)

    constructor(private http: HttpClient) { }

    create(dto: CreateSolicitudeDto){
      return this.http.post(`${this.apiUrl}`, dto)
    }

    getAllSolicitudes() {
      return this.http.get<SolicitudeGetDto[]>(`${this.apiUrl}`).pipe(
          map((data: SolicitudeGetDto[]) => {
            return data.map(this.solicitudeDtoToModel)
          })
      )
    }

    updateSolicitude(id: number, solicitude: Solicitude){
      let solicitudePutDto :SolicitudePutDto = {
        state : solicitude.state
      }
      return this.http.put<SolicitudeGetDto>(`${this.apiUrl}/${id}`, solicitudePutDto).pipe(
          map((data: SolicitudeGetDto) => this.solicitudeDtoToModel(data))
      )
    }

    private solicitudeDtoToModel(data: SolicitudeGetDto) {
      let state : any = SolicitudeState[data.state]
      let solicitude: Solicitude = {
        id: data.id,
        state: state,
        date: data.date,
        employeeUserName: data.employeeUserName,
        pharmacy: data.pharmacy,
        solicitudeItems: data.solicitudeItems,
      }
      return solicitude;
    }
}
