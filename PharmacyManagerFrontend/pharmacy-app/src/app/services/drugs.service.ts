import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Drug} from "../models/drug.model";

@Injectable({
  providedIn: 'root'
})
export class DrugsService {

  constructor( private http: HttpClient) { }

  getAllDrugs() {
    return this.http.get<Drug[]>('')
  }
}
