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
  private userSource: BehaviorSubject<User>;
  private userStream: Observable<User>;

  private readonly httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  };

  constructor(private http: HttpClient, private router: Router) {
    this.userSource = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
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

  get currentUser(): User {
    return this.userSource.value;
  }

  login(login: UserLoginForm): Observable<User> {
    const authUrl = 'api/authenticate'
    return this.http.post<User>(authUrl, login, this.httpOptions).pipe(
      tap(user => {
        if (user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.userSource.next(user);
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
