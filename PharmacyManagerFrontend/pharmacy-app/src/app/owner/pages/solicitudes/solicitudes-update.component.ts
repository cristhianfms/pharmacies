import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { QuerySolicitudeDto } from 'src/app/models/Dto/solicitude-query.model';
import { Solicitude, SolicitudePutModel } from 'src/app/models/solicitude.model';
import { SolicitudesService } from 'src/app/services/solicitudes.service';

@Component({
  selector: 'app-solicitudes-update',
  templateUrl: './solicitudes-update.component.html',
  styleUrls: ['./solicitudes-update.component.scss']
})
export class SolicitudesUpdateComponent implements OnInit {
  solicitudes: Solicitude[] = [];

  solicitude: Solicitude = {
    id: 0,
    state: 'PENDING',
    date: new Date(''),
    employeeUserName: '',
    pharmacy: '',
    solicitudeItems: []
  }

  updatingStatus: 'loading' | 'success' | 'error' | null = null
  errorMessage: string = ''

  constructor(private solicitudeService: SolicitudesService, private router: Router) { }

  ngOnInit(): void {
    this.solicitudeService.getAllSolicitudes().subscribe({
      next: this.handleGetAllResponse.bind(this),
      error: this.handleError.bind(this)
        }
    )
    this.solicitudeService.selectedSolicitudeToEdit$.subscribe((selectedSolicitude)=>{
        if(selectedSolicitude){
          this.solicitude = selectedSolicitude
        }
    })
  }
  handleGetAllResponse(data: any){
    this.solicitudes = data
  }

  handleError(error: any){
    window.alert("Error getting solicitudes")
  }
  onAccept(solicitude: Solicitude) {
    this.updateSolicitudeStatus(solicitude, 'ACCEPTED')
  }

  isAcceptDisable(solicitude: Solicitude) {
    return solicitude.state === 'ACCEPTED'|| solicitude.state === 'REJECTED'
  }

  onReject(solicitude: Solicitude) {
    this.updateSolicitudeStatus(solicitude, 'REJECTED')
  }

  isRejectDisable(solicitude: Solicitude) {
    return solicitude.state === 'ACCEPTED'|| solicitude.state === 'REJECTED'
  }

  updateSolicitudeStatus(solicitude: Solicitude, state: any){
    this.updatingStatus = 'loading';
    let solicitudeToUpdate: SolicitudePutModel =
    {
      state: state
    } 
    this.solicitudeService.updateSolicitude(solicitude.id, solicitudeToUpdate)
    .subscribe({
      next: this.handleUpdateResponse.bind(this),
          error: this.handleUpdateError.bind(this)
    })
    
  }
  handleUpdateResponse(data: any){
    this.solicitude = data;
    this.updatingStatus = 'success'
    setTimeout(() => this.updatingStatus = null,2000)
  }

  handleUpdateError(error: any){
    this.updatingStatus = 'error'
    this.errorMessage = error.error.message
    setTimeout(() => this.updatingStatus = null,2000)
  }

}
