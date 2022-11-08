import { Component, Input, OnInit } from '@angular/core';
import { SolicitudeQueryDto } from 'src/app/models/Dto/solicitude-query.model';
import { Solicitude } from 'src/app/models/solicitude.model';
import { SolicitudesService } from 'src/app/services/solicitudes.service';

@Component({
  selector: 'app-solicitudes-list',
  templateUrl: './solicitudes-list.component.html',
  styleUrls: ['./solicitudes-list.component.scss']
})
export class SolicitudesListComponent implements OnInit {
  solicitudes: Solicitude[] = [];

  @Input() solicitudeQuery: SolicitudeQueryDto = {
    dateFrom: null,
    dateTo: null,
    state: null,
    drugCode: null
  }
  constructor(private solicitudeService: SolicitudesService) { }

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

}
