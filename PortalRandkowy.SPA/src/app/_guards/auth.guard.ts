import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private auuthService: AuthService, private router: Router, private alertify: AlertifyService) {}

  canActivate(): boolean {

    if(this.auuthService.logedin())
    {
      return true;
    }

    this.alertify.error('Nie masz uprawnień');
    this.router.navigate(['/home']);
    return false;
  }
}
