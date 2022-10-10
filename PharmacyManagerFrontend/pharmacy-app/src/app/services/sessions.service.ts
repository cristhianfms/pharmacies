import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Session} from "../models/session.model";
import {User} from "../models/user.model";
import {TokenService} from "./token.service";
import {BehaviorSubject, switchMap, tap} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class SessionsService {

  private apiUrl: string = `${environment.API_URL}/api/sessions`

  private user = new BehaviorSubject<User | null>(null);
  user$ = this.user.asObservable();

  constructor(private http: HttpClient, private tokenService: TokenService) { }

  login(userName: string, password: string){
    return this.http.post<Session>(this.apiUrl, {userName, password})
        .pipe(
            tap(response => this.tokenService.saveToken(response.token))
        )
  }

  getProfile(){
    return this.http.get<User>(`${this.apiUrl}/profile`)
        .pipe(
            tap(user => this.user.next(user))
        )
  }

  loginAndGet(userName: string, password: string){
    return this.login(userName, password)
        .pipe(
          switchMap(() => this.getProfile())
        )
  }
}
