import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SolicitudeQueryDto } from 'src/app/models/Dto/solicitude-query.model';

@Component({
  selector: 'app-solicitudes',
  templateUrl: './solicitudes.component.html',
  styleUrls: ['./solicitudes.component.scss']
})
export class SolicitudesComponent implements OnInit {

  filterDateFrom: string | null = null;
  filterDateTo: string | null = null;
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
  obtainSolicitudeQuery(event: Event){
    console.log("entra a obtain");
    event.preventDefault();
    let state = this.filterState == "All" ? null : this.filterState;
    console.log(state);
    this.solicitudeQuery = {
      dateFrom: this.filterDateFrom,
      dateTo: this.filterDateTo,
      state: state,
      drugCode: this.filterDrugCode
    }
    console.log(this.filterState);
  } 
  onCreateSolicitude(){
    this.router.navigate(['/employee/solicitudes-create']);
  }
}
