import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CreateInvitationDto, Invitation, UpdateInvitationDto} from "../models/invitation.model";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class InvitationsService {

  apiUrl: string = `${environment}/api/invitations`

  constructor(private http: HttpClient) { }

  create(dto: CreateInvitationDto){
    return this.http.post(`${this.apiUrl}`, dto)
  }

  update(id: number, dto: UpdateInvitationDto){
    return this.http.put<Invitation>(`${this.apiUrl}/${id}`, dto);
  }

  getInvitation(id: number){
    return this.http.get<Invitation>(`${this.apiUrl}/${id}`)
  }

  getAllInvitations() {
    return this.http.get<Invitation[]>(`${this.apiUrl}`)
  }
}
