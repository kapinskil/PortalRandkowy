import { Route } from '@angular/compiler/src/core';
import { Injectable } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { User } from '../_models/User';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';

@Injectable()

export class UserEditResolver implements Resolve<User> {
    constructor(private userService: UserService,
                private router: Router,
                private alertyfyService: AlertifyService,
                private authService: AuthService) {}


    resolve(route: ActivatedRouteSnapshot): Observable<User> {
       return this.userService.getUser(this.authService.decodeToken.nameid).pipe(
           catchError(error => {
            this.alertyfyService.error('problem z pobieraniem danych');
            this.router.navigate(['/uzytkownicy']);
            return of(null);
           })
       );
    }
}
