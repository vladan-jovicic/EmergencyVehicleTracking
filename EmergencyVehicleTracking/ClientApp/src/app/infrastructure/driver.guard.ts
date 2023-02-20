import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import { Observable } from 'rxjs';
import {UserService} from "../services/user.service";

@Injectable({
  providedIn: 'root'
})
export class DriverGuard implements CanActivate {

  constructor(private userService: UserService, private router: Router) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    const currentUser = this.userService.currentUser;
    if (!currentUser || !currentUser.token || !currentUser.roles) {
      this.userService.logout();
      return false;
    }

    if (!currentUser?.roles.includes("DriverUser")) {
      this.userService.logout();
      return false;
    }

    return true;
  }

}
