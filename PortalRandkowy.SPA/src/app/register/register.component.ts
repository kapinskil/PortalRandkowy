import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
declare let alertify: any;


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

 
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register(){
    this.authService.register(this.model).subscribe(() => {
      alertify.success("rejestracja udana");
    }, error => {
      alertify.error('wystąpił błąd rejestracji');
    });
  }

  cancel(){
    this.cancelRegister.emit(false);
    console.log(this.model);
  }
}
