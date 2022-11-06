import { Injectable } from '@angular/core';
import {catchError, throwError} from "rxjs";
import {DrugExporter} from "../models/drug-exporter.model";
import {environment} from "../../environments/environment";
import {HttpClient, HttpErrorResponse, HttpStatusCode} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class DrugsExporterService {

  apiUrl: string = `${environment.API_URL}/api/drug-exporters`

  constructor(private http: HttpClient) { }

  getAllExporters() {
    return this.http.get<DrugExporter[]>(`${this.apiUrl}`)
        .pipe(
            catchError((error: HttpErrorResponse) => {
              if (error.status === HttpStatusCode.InternalServerError) {
                return throwError(() => new Error("Issue with the server"));
              }
              return throwError(() => new Error("Something went wrong"))
            })
        )
  }

  export(exporter: DrugExporter) {
    return this.http.post<DrugExporter>(`${this.apiUrl}/export`, exporter)
        .pipe(
            catchError((error: HttpErrorResponse) => {
              if (error.status === HttpStatusCode.InternalServerError) {
                return throwError(() => new Error("Issue with the server"));
              }
              return throwError(() => new Error("Something went wrong"))
            })
        )
  }
}
