import { Component, OnInit } from '@angular/core';
import {Drug} from "../../models/drug.model";
import {DrugsService} from "../../services/drugs.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  drugs: Drug[] = []
  statusGetAll: 'loading' | 'success' | 'error' | 'init' = 'init'

  constructor(private drugService: DrugsService) { }

  ngOnInit(): void {
    this.statusGetAll = 'loading'
    this.drugService.getAllDrugs().subscribe({
          next: this.handleGetAllResponse.bind(this),
          error: this.handleError.bind(this)
        }
    )
  }

  handleGetAllResponse(data: any){
    this.drugs = data
    this.statusGetAll = 'success'
  }

  handleError(error: any){
    window.alert(error)
    this.statusGetAll = 'error'
  }

}
