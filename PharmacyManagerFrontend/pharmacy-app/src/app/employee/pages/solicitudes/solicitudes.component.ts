import { formatCurrency } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { QuerySolicitudeDto } from 'src/app/models/Dto/solicitude-query.model';

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

  solicitudeQuery: QuerySolicitudeDto = {
    dateFrom: null,
    dateTo: null,
    state: null,
    drugCode: null
  }

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  obtainSolicitudeQuery(event: Event){
    event.preventDefault();
    let state = this.filterState == "All" ? null : this.filterState;
    this.solicitudeQuery = {
      dateFrom: this.filterDateFrom,
      dateTo: this.filterDateTo,
      state: state,
      drugCode: this.filterDrugCode
    }
  }
  resetSolicitudeQuery(event: Event){
    let state = this.filterState == "All" ? null : this.filterState;
    this.solicitudeQuery = {
      dateFrom: null,
      dateTo: null,
      state: null,
      drugCode: null
    }
  }

  onCreateSolicitude(){
    this.router.navigate(['/employee/solicitudes-create']);
  }
}
