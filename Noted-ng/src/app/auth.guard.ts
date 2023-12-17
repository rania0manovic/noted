import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import { Observable } from 'rxjs';
import jwt_decode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router) { }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let token = localStorage.getItem("token");

    try {
      let token = localStorage.getItem("token");
      if(token==null) {
        this.router.navigateByUrl('/login');
        return false;
      }
      const decodedToken = jwt_decode(token);
      if(decodedToken)
        return true;
      else {this.router.navigateByUrl('/login');
       return false;}

    } catch (error:any) {
      this.router.navigateByUrl('/login');
      return false;
    }
  }
}
