import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CreateInvitationDto, Invitation, UpdateInvitationDto} from "../models/invitation.model";
import {environment} from "../../environments/environment";
import {BehaviorSubject, Subject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class InvitationsService {

  apiUrl: string = `${environment.API_URL}/api/invitations`

  selectedInvitationToEdit = new BehaviorSubject<Invitation | null>(null)
  selectedInvitationToEdit$ = this.selectedInvitationToEdit.asObservable();

  constructor(private http: HttpClient) { }

  create(dto: CreateInvitationDto){
    return this.http.post(`${this.apiUrl}`, dto)
  }

  update(id: string, invitation: Invitation){
    return this.http.put<Invitation>(`${this.apiUrl}/${id}`, invitation);
  }

  getAllInvitations() {
    return this.http.get<Invitation[]>(`${this.apiUrl}`)
  }
}
