import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SolicitudeQueryDto } from 'src/app/models/Dto/solicitude-query.model';

@Component({
  selector: 'app-solicitudes',
  templateUrl: './solicitudes.component.html',
  styleUrls: ['./solicitudes.component.scss']
})
export class SolicitudesComponent implements OnInit {

  filterDateFrom: Date | null = null;
  filterDateTo: Date | null = null;
  filterState: string  | null = null;
  filterDrugCode: string | null = null;

  solicitudeQuery: SolicitudeQueryDto = {
    dateFrom: null,
    dateTo: null,
    state:null,
    drugCode: null
  }

  constructor(private router: Router) { }

  ngOnInit(): void {
  }
  obtainSolicitudeQuery(){
    let state = this.filterState == "All" ? null : this.filterState;
  this.solicitudeQuery = {
    dateFrom: this.filterDateFrom,
    dateTo: this.filterDateTo,
    state: state,
    drugCode: this.filterDrugCode
  }
  } 
  onCreateSolicitude(){
    this.router.navigate(['/employee/solicitudes-create']);
  }
}
