import { Component, OnInit } from '@angular/core';
import {AlertService} from "../../services/alert.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})
export class AlertComponent implements OnInit {

  private subscription: Subscription | undefined = undefined;
  message: {cssClass: string, text?:string, type?: string, previewcsv: boolean, yesText?: string, noText?: string} = {
    cssClass: '',
    previewcsv: false
  };
  header = true;
  delimiter = ',';
  custom='';
  delimiterIndex = 1;

  constructor(private alertService: AlertService) { }

  ngOnInit() {
    this.subscription = this.alertService.getAlert()
      .subscribe(response => {
        switch (response && response.type) {
          case 'success':
            response.cssClass = 'alert alert-success';
            break;
          case 'error':
            response.cssClass = 'alert alert-danger';
            break;
          case 'prompt':
            response.cssClass = 'alert alert-primary';
            if(!response.yestext)
              response.yestext = 'Yes';
            if(!response.notext)
              response.notext = 'No';
            break;
        }

        this.message = response;
      });
  }

  isAlertVisible(): boolean {
    return this.alertService.alertVisible;
  }

  ngOnDestroy() {
    (this.subscription && this.subscription.unsubscribe());
  }

  accept(){
    if (this.delimiterIndex === 4) this.delimiter = this.custom;
    this.alertService.accept(this.header, this.delimiter);
    this.delimiter = ',';
    this.delimiterIndex = 1;
    this.custom = '';
    this.header = true;
  }

  reject(){
    this.alertService.reject();
    this.delimiter = ',';
    this.delimiterIndex = 1;
    this.custom = '';
    this.header = true;
  }

  setActive(index: number, delimiter: string) {
    this.delimiter = delimiter;
    this.delimiterIndex = index;
  }

}
