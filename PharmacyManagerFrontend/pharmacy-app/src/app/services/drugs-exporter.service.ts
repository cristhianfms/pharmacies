import { Injectable } from '@angular/core';
import {DrugExporter} from "../models/drug-exporter.model";
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class DrugsExporterService {

  apiUrl: string = `${environment.API_URL}/api/drug-exporters`

  constructor(private http: HttpClient) { }

  getAllExporters() {
    return this.http.get<DrugExporter[]>(`${this.apiUrl}`)
  }

  export(exporter: DrugExporter) {
    return this.http.post<DrugExporter>(`${this.apiUrl}/export`, exporter)
  }
}
