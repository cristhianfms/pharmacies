import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {map, Observable} from 'rxjs';
import {SessionsService} from "../services/sessions.service";

@Injectable({
  providedIn: 'root'
})
export class EmployeeGuard implements CanActivate {

  constructor(
      private sessionService: SessionsService,
      private router: Router
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.sessionService.user$
        .pipe(
            map(user => {
              if(user?.roleName === 'Employee') {
                return true;
              } else {
                this.router.navigate(['/home']);
                return false;
              }
            })
        )
  }

}
