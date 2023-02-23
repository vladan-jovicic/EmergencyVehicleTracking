import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from "rxjs";
import {User} from "../models/user";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Router} from "@angular/router";
import {UserLoginForm} from "../models/user-login-form";
import {tap} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private rootUrl = 'api/users';
  private userSource: BehaviorSubject<User | undefined>;
  private userStream: Observable<User | undefined>;

  private readonly httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  };

  constructor(private http: HttpClient, private router: Router) {
    const currentUserSerialized = localStorage.getItem('currentUser');
    if (currentUserSerialized !== undefined && currentUserSerialized != null) {
      const currentUser = JSON.parse(currentUserSerialized);
      this.userSource = new BehaviorSubject<User | undefined>(currentUser);
    } else {
      this.userSource = new BehaviorSubject<User | undefined>(undefined);
    }
    this.userStream = this.userSource.asObservable();

    if (!this.currentUser) {
      if (location.pathname === '/' || location.pathname === '/login') {
        this.router.navigate(['/login']);
      }
      else {
        this.router.navigate(['/login'], { queryParams: { returnUrl: location.pathname } });
      }
    }
  }

  get currentUser(): User | undefined {
    return this.userSource.value;
  }

  getUserHomeUrl(): string {
    if (this.currentUser && this.currentUser.token && this.currentUser.roles) {
      if (this.currentUser.roles.includes("DriverUser")) {
        return '/driver-home';
      } else {
        return '/dashboard';
      }
    }

    return '/login';
  }

  login(login: UserLoginForm): Observable<User> {
    const authUrl = 'api/v1/Authorize'
    return this.http.post<User>(authUrl, login, this.httpOptions).pipe(
      tap(user => {
        if (user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.userSource.next(user);
          const nextUri = this.getUserHomeUrl();
          this.router.navigate([nextUri]);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.userSource.next(undefined);
    this.router.navigate(['/login']);
    // .then(r => this.progressService.stopProgress());
  }
}
