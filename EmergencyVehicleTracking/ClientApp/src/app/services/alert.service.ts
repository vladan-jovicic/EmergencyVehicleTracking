import { Injectable } from '@angular/core';
import {Observable, Subject} from "rxjs";
import {NavigationStart, Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})

export class AlertService {
  private subject = new Subject<any>();
  private keepAfterRouteChange = false;
  private alertTimer: number | undefined;
  public alertVisible: boolean = false;
  public dialog: boolean = false;

  constructor(private router: Router) {
    // clear alert messages on route change unless 'keepAfterRouteChange' flag is true
    this.router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        if (this.keepAfterRouteChange) {
          // only keep for a single route change
          this.keepAfterRouteChange = false;
        } else {
          // clear alert message
          this.clear();
        }
      }
    });
  }

  getAlert(): Observable<any> {
    return this.subject.asObservable();
  }

  success(message: string, keepAfterRouteChange = false, autohide: number | boolean = false) {
    this.keepAfterRouteChange = keepAfterRouteChange;
    this.subject.next({ type: 'success', text: message });
    this.manageVisibility(autohide);
    this.dialog = false;
  }

  error(message: string, keepAfterRouteChange = false, autohide: number | boolean = false) {
    this.keepAfterRouteChange = keepAfterRouteChange;
    this.subject.next({ type: 'error', text: message });
    this.manageVisibility(autohide);
    this.dialog = false;
  }

  prompt(message: string, keepAfterRouteChange = false, yestext = 'Yes', notext='No', previewcsv = false) {
    this.keepAfterRouteChange = keepAfterRouteChange;
    this.subject.next({ type: 'prompt', text: message, yestext:yestext, notext:notext, previewcsv: previewcsv });
    this.manageVisibility(false);
    this.dialog = true;
  }

  accept(header: boolean, delimiter: string){
    this.alertVisible = false;
    this.subject.next({ type: 'accept', header: header, delimiter: delimiter});
  }

  reject(){
    this.alertVisible = false;
    this.subject.next({ type: 'reject'});
  }

  clear() {
    // clear by calling subject.next() without parameters
    this.subject.next(null);
  }

  private manageVisibility(autohide: number | boolean = false) {

    this.alertVisible = true;

    if (this.alertTimer) {
      window.clearTimeout(this.alertTimer);
      this.alertTimer = undefined;
      this.clear();
    }

    if (autohide) {
      this.alertTimer = window.setTimeout(() => {
        this.alertVisible = false;
        setTimeout(() => {
          this.alertTimer = undefined;
          this.clear();
        }, 600);
      }, (typeof autohide === 'number' ? autohide : 3000));
    }
  }
}
