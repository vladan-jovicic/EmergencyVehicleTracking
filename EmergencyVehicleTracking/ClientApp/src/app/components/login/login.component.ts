import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {UserLoginForm} from "../../models/user-login-form";
import {UserService} from "../../services/user.service";
import {AlertService} from "../../services/alert.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  loading = false;
  returnUrl: string = '';
  error = '';
  version: string = '23.0217';

  constructor(private userService: UserService,
              private alertService: AlertService,
              private formBuilder: FormBuilder,
              private router: Router,
              private route: ActivatedRoute) {
    if (this.userService.currentUser) {
      this.router.navigate(['/folders']);
    }

    this.loginForm = this.formBuilder.group({
      username: this.formBuilder.control(null, Validators.required),
      password: this.formBuilder.control(null, Validators.required)
    });
  }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
  }

  isAlertVisible() {
    return this.alertService.alertVisible;
  }

  get controls() {
    return this.loginForm.controls;
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      return;
    }
    this.loading = true;

    const login = this.loginForm.value as UserLoginForm;
    this.userService.login(login).subscribe(
      _ => {
        this.router.navigate([this.returnUrl]);
      },
      _ => {
        this.alertService.error('Error logging in.<br>Please check your credentials.', false, true);
        this.loading = false;
      }
    );
  }

}
