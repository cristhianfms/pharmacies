import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { QuerySolicitudeDto } from 'src/app/models/Dto/solicitude-query.model';
import { Solicitude } from 'src/app/models/solicitude.model';
import { SolicitudesService } from 'src/app/services/solicitudes.service';

@Component({
  selector: 'app-solicitudes-update',
  templateUrl: './solicitudes-update.component.html',
  styleUrls: ['./solicitudes-update.component.scss']
})
export class SolicitudesUpdateComponent implements OnInit {
  solicitudes: Solicitude[] = [];


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

  updateSolicitudeStatus(solicitude: Solicitude, status: any){
    this.updatingStatus = 'loading'
   /* let itemToUpdate: PurchaseItem = { ...item }
    itemToUpdate.state = status
    let purchaseToUpdate: Purchase = {...this.purchase}
    purchaseToUpdate.items = [itemToUpdate]
    this.purchasesService.updatePurchase(this.purchase.id, purchaseToUpdate).subscribe({
          next: this.handleUpdateResponse.bind(this),
          error: this.handleUpdateError.bind(this)
        }
    )*/
  }
}
