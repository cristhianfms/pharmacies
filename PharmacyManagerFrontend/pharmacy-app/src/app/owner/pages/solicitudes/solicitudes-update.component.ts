import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Solicitude } from 'src/app/models/solicitude.model';
import { SolicitudesService } from 'src/app/services/solicitudes.service';

@Component({
  selector: 'app-solicitudes-update',
  templateUrl: './solicitudes-update.component.html',
  styleUrls: ['./solicitudes-update.component.scss']
})
export class SolicitudesUpdateComponent implements OnInit {
  solicitudes: Solicitude[] = [];

  selectedSolicitude: Solicitude = {
    id: 0,
    state: "Pending",
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
  }
  handleGetAllResponse(data: any){
    this.solicitudes = data
  }

  handleError(error: any){
    window.alert("Error getting solicitudes")
  }
  onAccept(solicitude: Solicitude) {
    this.updateSolicitudeStatus(solicitude, 'Accepted')
  }

  isAcceptDisable(solicitude: Solicitude) {
    return solicitude.state === 'Accepted'|| solicitude.state === 'Rejected'
  }

  onReject(solicitude: Solicitude) {
    this.updateSolicitudeStatus(solicitude, 'Rejected')
  }

  isRejectDisable(solicitude: Solicitude) {
    return solicitude.state === 'Accepted'|| solicitude.state === 'Rejected'
  }

  updateSolicitudeStatus(solicitude: Solicitude, state: any){
    this.updatingStatus = 'loading'
    this.selectedSolicitude = solicitude
    let solicitudeToUpdate: Solicitude = {...solicitude}
    solicitudeToUpdate.state = state
    this.solicitudeService.updateSolicitude(solicitude.id, solicitudeToUpdate)
    .subscribe({
      next: this.handleUpdateResponse.bind(this),
      error: this.handleUpdateError.bind(this)
    })

  }
  handleUpdateResponse(data: any){
    this.selectedSolicitude.state = data.state;
    this.updatingStatus = 'success'
    setTimeout(() => this.updatingStatus = null,2000)
  }

  handleUpdateError(error: any){
    this.updatingStatus = 'error'
    this.errorMessage = error.error.message
    setTimeout(() => this.updatingStatus = null,2000)
  }

}
