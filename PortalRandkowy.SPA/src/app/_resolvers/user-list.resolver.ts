import { Route } from '@angular/compiler/src/core';
import { Injectable } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { User } from '../_models/User';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';

@Injectable()
export class UserListResolver implements Resolve<User[]> {
    
    pageNumber = 1;
    pageSize = 100;

    constructor(private userService: UserService,
                private router: Router,
                private alertyfyService: AlertifyService) {}


    resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
       return this.userService.getUsers(this.pageNumber, this.pageSize).pipe(
           catchError(error => {
            this.alertyfyService.error('problem z pobieraniem danych');
            this.router.navigate(['/home']);
            return of(null);
           })
       );
    }
}
